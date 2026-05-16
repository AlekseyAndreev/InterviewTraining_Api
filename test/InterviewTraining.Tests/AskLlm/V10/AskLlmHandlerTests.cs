using System.Threading.Tasks;
using InterviewTraining.Application.AskLlm.V10;
using InterviewTraining.Application.Interfaces;
using Moq;
using Xunit;

namespace InterviewTraining.Tests.AskLlm.V10;

public class AskLlmHandlerTests : BaseUnitTests
{
    private readonly Mock<ILlmService> _llmServiceMock;
    private readonly AskLlmHandler _sut;

    public AskLlmHandlerTests()
    {
        _llmServiceMock = new Mock<ILlmService>();
        _sut = new AskLlmHandler(_llmServiceMock.Object);
    }

    [Fact]
    public async Task HandleAsync_WithoutSystemPrompt_CallsGetCompletionAsyncWithPromptOnly()
    {
        // Arrange
        var request = new AskLlmRequest { UserText = "Расскажи про C#", InterviewType = LlmInterviewType.Dotnet };
        _llmServiceMock
            .Setup(s => s.GetCompletionAsync(request.InterviewType, request.UserText, Token))
            .ReturnsAsync("C# — это язык программирования");

        // Act
        var result = await _sut.HandleAsync(request, Token);

        // Assert
        Assert.Equal("C# — это язык программирования", result.Answer);
        _llmServiceMock.Verify(s => s.GetCompletionAsync(request.InterviewType, request.UserText, Token), Times.Once);
    }
}