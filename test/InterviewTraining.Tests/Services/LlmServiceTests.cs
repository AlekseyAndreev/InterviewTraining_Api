using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using InterviewTraining.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InterviewTraining.Tests.Services;

public class LlmServiceTests : BaseUnitTests
{
    private readonly Mock<ILogger<LlmService>> _loggerMock;
    private readonly Mock<IConfiguration> _configurationMock;

    public LlmServiceTests()
    {
        _loggerMock = new Mock<ILogger<LlmService>>();
        _configurationMock = new Mock<IConfiguration>();

        _configurationMock.Setup(c => c["Llm:ModelId"]).Returns("minimax-m2.5-free");
        _configurationMock.Setup(c => c["Llm:ApiKey"]).Returns("test-api-key");
    }

    [Fact]
    public async Task GetCompletionAsync_WithPromptOnly_SendsCorrectRequest()
    {
        // Arrange
        var responseBody = JsonSerializer.Serialize(new
        {
            choices = new[]
            {
                new
                {
                    message = new { role = "assistant", content = "Тестовый ответ" }
                }
            }
        });

        var handler = new MockHttpMessageHandler(responseBody, HttpStatusCode.OK);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://opencode.ai/zen/v1/")
        };

        var sut = new LlmService(httpClient, _configurationMock.Object, _loggerMock.Object);

        // Act
        var result = await sut.GetCompletionAsync(Application.AskLlm.V10.LlmInterviewType.Dotnet, "Привет!", Token);

        // Assert
        Assert.Equal("Тестовый ответ", result);
    }

    [Fact]
    public async Task GetCompletionAsync_WhenApiReturnsError_ThrowsHttpRequestException()
    {
        // Arrange
        var handler = new MockHttpMessageHandler("Internal Server Error", HttpStatusCode.InternalServerError);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://opencode.ai/zen/v1/")
        };

        var sut = new LlmService(httpClient, _configurationMock.Object, _loggerMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => sut.GetCompletionAsync(Application.AskLlm.V10.LlmInterviewType.Dotnet, "Привет!", Token));
    }

    [Fact]
    public void GetCompletionAsync_WhenApiKeyMissing_ThrowsInvalidOperationException()
    {
        // Arrange
        _configurationMock.Setup(c => c["Llm:ApiKey"]).Returns((string)null);
        var httpClient = new HttpClient { BaseAddress = new Uri("https://opencode.ai/zen/v1/") };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => new LlmService(httpClient, _configurationMock.Object, _loggerMock.Object));
    }

    [Fact]
    public async Task GetCompletionAsync_SendsAuthorizationHeader()
    {
        // Arrange
        var responseBody = JsonSerializer.Serialize(new
        {
            choices = new[]
            {
                new { message = new { role = "assistant", content = "Ответ" } }
            }
        });

        var handler = new MockHttpMessageHandler(responseBody, HttpStatusCode.OK);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://opencode.ai/zen/v1/")
        };

        var sut = new LlmService(httpClient, _configurationMock.Object, _loggerMock.Object);

        // Act
        await sut.GetCompletionAsync(Application.AskLlm.V10.LlmInterviewType.Dotnet, "Привет!", Token);

        // Assert
        Assert.Equal("Bearer", handler.LastRequest.Headers.Authorization.Scheme);
        Assert.Equal("test-api-key", handler.LastRequest.Headers.Authorization.Parameter);
    }

    [Fact]
    public async Task GetCompletionAsync_SendsCorrectModelInRequestBody()
    {
        // Arrange
        var responseBody = JsonSerializer.Serialize(new
        {
            choices = new[]
            {
                new { message = new { role = "assistant", content = "Ответ" } }
            }
        });

        var handler = new MockHttpMessageHandler(responseBody, HttpStatusCode.OK);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://opencode.ai/zen/v1/")
        };

        var sut = new LlmService(httpClient, _configurationMock.Object, _loggerMock.Object);

        // Act
        await sut.GetCompletionAsync(Application.AskLlm.V10.LlmInterviewType.Dotnet, "Привет!", Token);

        // Assert
        var requestBody = JsonSerializer.Deserialize<JsonElement>(handler.LastRequestBody);
        Assert.Equal("minimax-m2.5-free", requestBody.GetProperty("model").GetString());
    }

    /// <summary>
    /// Mock HttpMessageHandler для перехвата HTTP-запросов
    /// </summary>
    private class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _response;
        private readonly HttpStatusCode _statusCode;

        public HttpRequestMessage LastRequest { get; private set; }
        public string LastRequestBody { get; private set; }

        public MockHttpMessageHandler(string response, HttpStatusCode statusCode)
        {
            _response = response;
            _statusCode = statusCode;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            if (request.Content != null)
            {
                LastRequestBody = await request.Content.ReadAsStringAsync(cancellationToken);
            }

            return new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_response, Encoding.UTF8, "application/json")
            };
        }
    }
}