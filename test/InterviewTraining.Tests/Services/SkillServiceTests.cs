using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewTraining.Application.GetSkillsTree.V10;
using InterviewTraining.Domain;
using InterviewTraining.Infrastructure.DatabaseContext;
using InterviewTraining.Infrastructure.Repositories.Interfaces;
using InterviewTraining.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InterviewTraining.Tests.Services;

public class SkillServiceTests : BaseUnitTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ISkillGroupRepository> _skillGroupRepositoryMock;
    private readonly IMemoryCache _cache;
    private readonly Mock<ILogger<SkillService>> _loggerMock;
    private readonly SkillService _skillService;

    public SkillServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _skillGroupRepositoryMock = new Mock<ISkillGroupRepository>();
        _cache = new MemoryCache(new MemoryCacheOptions());
        _loggerMock = new Mock<ILogger<SkillService>>();

        _unitOfWorkMock.Setup(u => u.SkillGroups).Returns(_skillGroupRepositoryMock.Object);

        _skillService = new SkillService(_unitOfWorkMock.Object, _cache, _loggerMock.Object);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_ReturnsEmptyTree_WhenNoGroupsExist()
    {
        // Arrange
        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(new List<SkillGroup>());

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Groups);
        Assert.Empty(result.Groups);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_ReturnsRootGroups_WhenGroupsExist()
    {
        // Arrange
        var groups = new List<SkillGroup>
        {
            new SkillGroup { Id = Guid.NewGuid(), Name = "Root Group 1", ParentGroupId = null },
            new SkillGroup { Id = Guid.NewGuid(), Name = "Root Group 2", ParentGroupId = null }
        };

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(groups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Groups.Count);
        Assert.Contains(result.Groups, g => g.Name == "Root Group 1");
        Assert.Contains(result.Groups, g => g.Name == "Root Group 2");
    }

    [Fact]
    public async Task GetSkillsTreeAsync_ReturnsNestedGroups_WhenHierarchyExists()
    {
        // Arrange
        var rootGroupId = Guid.NewGuid();
        var childGroupId = Guid.NewGuid();

        var groups = new List<SkillGroup>
        {
            new SkillGroup
            {
                Id = rootGroupId,
                Name = "Root Group",
                ParentGroupId = null,
                ChildGroups = new List<SkillGroup>
                {
                    new SkillGroup { Id = childGroupId, Name = "Child Group", ParentGroupId = rootGroupId }
                }
            },
            new SkillGroup { Id = childGroupId, Name = "Child Group", ParentGroupId = rootGroupId }
        };

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(groups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        Assert.Single(result.Groups);
        Assert.Single(result.Groups[0].ChildGroups);
        Assert.Equal("Child Group", result.Groups[0].ChildGroups[0].Name);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_ReturnsSkillsInGroups_WhenSkillsExist()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var skillId = Guid.NewGuid();

        var groups = new List<SkillGroup>
        {
            new SkillGroup
            {
                Id = groupId,
                Name = "Group with Skills",
                ParentGroupId = null,
                Skills = new List<Skill>
                {
                    new Skill { Id = skillId, Name = "Skill 1", GroupId = groupId, IsDeleted = false }
                }
            }
        };

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(groups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        Assert.Single(result.Groups);
        Assert.Single(result.Groups[0].Skills);
        Assert.Equal("Skill 1", result.Groups[0].Skills[0].Name);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_FiltersDeletedSkills()
    {
        // Arrange
        var groupId = Guid.NewGuid();

        var groups = new List<SkillGroup>
        {
            new SkillGroup
            {
                Id = groupId,
                Name = "Group",
                ParentGroupId = null,
                Skills = new List<Skill>
                {
                    new Skill { Id = Guid.NewGuid(), Name = "Active Skill", GroupId = groupId, IsDeleted = false },
                    new Skill { Id = Guid.NewGuid(), Name = "Deleted Skill", GroupId = groupId, IsDeleted = true }
                }
            }
        };

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(groups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        Assert.Single(result.Groups[0].Skills);
        Assert.Equal("Active Skill", result.Groups[0].Skills[0].Name);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_ReturnsDataFromCache_WhenCacheExists()
    {
        // Arrange
        var cachedResponse = new GetSkillsTreeResponse
        {
            Groups = new List<SkillGroupDto>
            {
                new SkillGroupDto { Id = Guid.NewGuid(), Name = "Cached Group" }
            }
        };

        _cache.Set("SkillsTree", cachedResponse);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Groups);
        Assert.Equal("Cached Group", result.Groups[0].Name);

        // Verify repository was not called
        _skillGroupRepositoryMock.Verify(r => r.GetFullTreeAsync(Token), Times.Never);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_SavesToCache_AfterDatabaseCall()
    {
        // Arrange
        var groups = new List<SkillGroup>
        {
            new SkillGroup { Id = Guid.NewGuid(), Name = "Root Group", ParentGroupId = null }
        };

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(groups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert - Check that data is now in cache
        Assert.True(_cache.TryGetValue("SkillsTree", out GetSkillsTreeResponse cachedResponse));
        Assert.NotNull(cachedResponse);
        Assert.Single(cachedResponse.Groups);
        Assert.Equal("Root Group", cachedResponse.Groups[0].Name);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_ReturnsComplexHierarchy_WhenMultipleLevelsExist()
    {
        // Arrange
        var rootId = Guid.NewGuid();
        var level1Id = Guid.NewGuid();
        var level2Id = Guid.NewGuid();

        var groups = new List<SkillGroup>
        {
            new SkillGroup { Id = rootId, Name = "Root", ParentGroupId = null },
            new SkillGroup { Id = level1Id, Name = "Level 1", ParentGroupId = rootId },
            new SkillGroup { Id = level2Id, Name = "Level 2", ParentGroupId = level1Id }
        };

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(groups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        Assert.Single(result.Groups); // One root
        Assert.Single(result.Groups[0].ChildGroups); // One level 1
        Assert.Single(result.Groups[0].ChildGroups[0].ChildGroups); // One level 2
        Assert.Equal("Level 2", result.Groups[0].ChildGroups[0].ChildGroups[0].Name);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_ReturnsMultipleRootGroups_WhenMultipleRootsExist()
    {
        // Arrange
        var root1Id = Guid.NewGuid();
        var root2Id = Guid.NewGuid();
        var child1Id = Guid.NewGuid();
        var child2Id = Guid.NewGuid();

        var groups = new List<SkillGroup>
        {
            new SkillGroup { Id = root1Id, Name = "Root 1", ParentGroupId = null },
            new SkillGroup { Id = root2Id, Name = "Root 2", ParentGroupId = null },
            new SkillGroup { Id = child1Id, Name = "Child 1", ParentGroupId = root1Id },
            new SkillGroup { Id = child2Id, Name = "Child 2", ParentGroupId = root2Id }
        };

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(groups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        Assert.Equal(2, result.Groups.Count);
        
        var root1 = result.Groups.First(g => g.Name == "Root 1");
        var root2 = result.Groups.First(g => g.Name == "Root 2");
        
        Assert.Single(root1.ChildGroups);
        Assert.Single(root2.ChildGroups);
        Assert.Equal("Child 1", root1.ChildGroups[0].Name);
        Assert.Equal("Child 2", root2.ChildGroups[0].Name);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_ReturnsSkillsWithCorrectIds()
    {
        // Arrange
        var groupId = Guid.NewGuid();
        var skillId1 = Guid.NewGuid();
        var skillId2 = Guid.NewGuid();

        var groups = new List<SkillGroup>
        {
            new SkillGroup
            {
                Id = groupId,
                Name = "Group",
                ParentGroupId = null,
                Skills = new List<Skill>
                {
                    new Skill { Id = skillId1, Name = "Skill 1", GroupId = groupId, IsDeleted = false },
                    new Skill { Id = skillId2, Name = "Skill 2", GroupId = groupId, IsDeleted = false }
                }
            }
        };

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(groups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        Assert.Equal(2, result.Groups[0].Skills.Count);
        Assert.Contains(result.Groups[0].Skills, s => s.Id == skillId1);
        Assert.Contains(result.Groups[0].Skills, s => s.Id == skillId2);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectRootGroups()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        // Link skills to groups
        foreach (var skill in allSkills)
        {
            skill.Group = allGroups.FirstOrDefault(g => g.Id == skill.GroupId);
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4, result.Groups.Count); // Development, Analytics, Testing, DevOps
        
        var groupNames = result.Groups.Select(g => g.Name).ToList();
        Assert.Contains("Разработка", groupNames);
        Assert.Contains("Аналитика", groupNames);
        Assert.Contains("Тестирование", groupNames);
        Assert.Contains("DevOps", groupNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectDevelopmentHierarchy()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        foreach (var skill in allSkills)
        {
            skill.Group = allGroups.FirstOrDefault(g => g.Id == skill.GroupId);
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        var developmentGroup = result.Groups.First(g => g.Name == "Разработка");
        Assert.NotNull(developmentGroup);
        Assert.Equal(4, developmentGroup.ChildGroups.Count); // Backend, Frontend, Full Stack, Mobile
        
        var childGroupNames = developmentGroup.ChildGroups.Select(g => g.Name).ToList();
        Assert.Contains("Backend", childGroupNames);
        Assert.Contains("Frontend", childGroupNames);
        Assert.Contains("Full Stack", childGroupNames);
        Assert.Contains("Mobile", childGroupNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectBackendSubGroups()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        foreach (var skill in allSkills)
        {
            skill.Group = allGroups.FirstOrDefault(g => g.Id == skill.GroupId);
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        var developmentGroup = result.Groups.First(g => g.Name == "Разработка");
        var backendGroup = developmentGroup.ChildGroups.First(g => g.Name == "Backend");
        
        Assert.Equal(9, backendGroup.ChildGroups.Count); // .NET, Java, Python, Node.js, Go, PHP, Ruby, C/C++, Rust
        
        var backendSubGroupNames = backendGroup.ChildGroups.Select(g => g.Name).ToList();
        Assert.Contains(".NET", backendSubGroupNames);
        Assert.Contains("Java", backendSubGroupNames);
        Assert.Contains("Python", backendSubGroupNames);
        Assert.Contains("Node.js", backendSubGroupNames);
        Assert.Contains("Go", backendSubGroupNames);
        Assert.Contains("PHP", backendSubGroupNames);
        Assert.Contains("Ruby", backendSubGroupNames);
        Assert.Contains("C/C++", backendSubGroupNames);
        Assert.Contains("Rust", backendSubGroupNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectDotNetSkills()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        // Link skills to groups (simulating EF Core navigation property loading)
        foreach (var group in allGroups)
        {
            group.Skills = allSkills.Where(s => s.GroupId == group.Id).ToList();
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        var developmentGroup = result.Groups.First(g => g.Name == "Разработка");
        var backendGroup = developmentGroup.ChildGroups.First(g => g.Name == "Backend");
        var dotNetGroup = backendGroup.ChildGroups.First(g => g.Name == ".NET");
        Assert.NotEmpty(dotNetGroup.Skills);
        var skillNames = dotNetGroup.Skills.Select(s => s.Name).ToList();
        Assert.Contains("C#", skillNames);
        Assert.Contains("ASP.NET Core", skillNames);
        Assert.Contains("Entity Framework", skillNames);
        Assert.Contains("Blazor", skillNames);
        Assert.Contains("WPF", skillNames);
        Assert.Contains("LINQ", skillNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectFrontendHierarchy()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        foreach (var skill in allSkills)
        {
            skill.Group = allGroups.FirstOrDefault(g => g.Id == skill.GroupId);
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        var developmentGroup = result.Groups.First(g => g.Name == "Разработка");
        var frontendGroup = developmentGroup.ChildGroups.First(g => g.Name == "Frontend");
        
        Assert.Equal(6, frontendGroup.ChildGroups.Count); // React, Angular, Vue.js, JavaScript, TypeScript, CSS/Стилизация
        
        var frontendSubGroupNames = frontendGroup.ChildGroups.Select(g => g.Name).ToList();
        Assert.Contains("React", frontendSubGroupNames);
        Assert.Contains("Angular", frontendSubGroupNames);
        Assert.Contains("Vue.js", frontendSubGroupNames);
        Assert.Contains("JavaScript", frontendSubGroupNames);
        Assert.Contains("TypeScript", frontendSubGroupNames);
        Assert.Contains("CSS/Стилизация", frontendSubGroupNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectMobileHierarchy()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        foreach (var skill in allSkills)
        {
            skill.Group = allGroups.FirstOrDefault(g => g.Id == skill.GroupId);
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        var developmentGroup = result.Groups.First(g => g.Name == "Разработка");
        var mobileGroup = developmentGroup.ChildGroups.First(g => g.Name == "Mobile");
        
        Assert.Equal(3, mobileGroup.ChildGroups.Count); // iOS, Android, Кроссплатформенная разработка
        
        var mobileSubGroupNames = mobileGroup.ChildGroups.Select(g => g.Name).ToList();
        Assert.Contains("iOS", mobileSubGroupNames);
        Assert.Contains("Android", mobileSubGroupNames);
        Assert.Contains("Кроссплатформенная разработка", mobileSubGroupNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectTestingHierarchy()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        foreach (var skill in allSkills)
        {
            skill.Group = allGroups.FirstOrDefault(g => g.Id == skill.GroupId);
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        var testingGroup = result.Groups.First(g => g.Name == "Тестирование");
        Assert.Equal(4, testingGroup.ChildGroups.Count); // Ручное тестирование, Автотестирование, Нагрузочное тестирование, Тестирование безопасности
        
        var testingSubGroupNames = testingGroup.ChildGroups.Select(g => g.Name).ToList();
        Assert.Contains("Ручное тестирование", testingSubGroupNames);
        Assert.Contains("Автотестирование", testingSubGroupNames);
        Assert.Contains("Нагрузочное тестирование", testingSubGroupNames);
        Assert.Contains("Тестирование безопасности", testingSubGroupNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectDevOpsHierarchy()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        foreach (var skill in allSkills)
        {
            skill.Group = allGroups.FirstOrDefault(g => g.Id == skill.GroupId);
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        var devOpsGroup = result.Groups.First(g => g.Name == "DevOps");
        
        Assert.Equal(5, devOpsGroup.ChildGroups.Count); // CI/CD, Облачные платформы, Контейнеризация, Мониторинг и логирование, Инфраструктура
        
        var devOpsSubGroupNames = devOpsGroup.ChildGroups.Select(g => g.Name).ToList();
        Assert.Contains("CI/CD", devOpsSubGroupNames);
        Assert.Contains("Облачные платформы", devOpsSubGroupNames);
        Assert.Contains("Контейнеризация", devOpsSubGroupNames);
        Assert.Contains("Мониторинг и логирование", devOpsSubGroupNames);
        Assert.Contains("Инфраструктура", devOpsSubGroupNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectTotalGroupsCount()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        foreach (var skill in allSkills)
        {
            skill.Group = allGroups.FirstOrDefault(g => g.Id == skill.GroupId);
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert - Count all groups in hierarchy
        var totalGroups = CountAllGroups(result.Groups);
        Assert.Equal(allGroups.Count, totalGroups);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectTotalSkillsCount()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        // Link skills to groups (simulating EF Core navigation property loading)
        foreach (var group in allGroups)
        {
            group.Skills = allSkills.Where(s => s.GroupId == group.Id).ToList();
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert - Count all skills in hierarchy
        var totalSkills = CountAllSkills(result.Groups);
        Assert.Equal(allSkills.Count, totalSkills);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectJavaSkills()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        // Link skills to groups (simulating EF Core navigation property loading)
        foreach (var group in allGroups)
        {
            group.Skills = allSkills.Where(s => s.GroupId == group.Id).ToList();
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        var developmentGroup = result.Groups.First(g => g.Name == "Разработка");
        var backendGroup = developmentGroup.ChildGroups.First(g => g.Name == "Backend");
        var javaGroup = backendGroup.ChildGroups.First(g => g.Name == "Java");
        Assert.NotEmpty(javaGroup.Skills);

        var skillNames = javaGroup.Skills.Select(s => s.Name).ToList();
        Assert.Contains("Java Core", skillNames);
        Assert.Contains("Spring Framework", skillNames);
        Assert.Contains("Spring Boot", skillNames);
        Assert.Contains("Hibernate", skillNames);
        Assert.Contains("Kotlin", skillNames);
        Assert.Contains("Maven", skillNames);
        Assert.Contains("Gradle", skillNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectReactSkills()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        // Link skills to groups (simulating EF Core navigation property loading)
        foreach (var group in allGroups)
        {
            group.Skills = allSkills.Where(s => s.GroupId == group.Id).ToList();
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        var developmentGroup = result.Groups.First(g => g.Name == "Разработка");
        var frontendGroup = developmentGroup.ChildGroups.First(g => g.Name == "Frontend");
        var reactGroup = frontendGroup.ChildGroups.First(g => g.Name == "React");
        Assert.NotEmpty(reactGroup.Skills);

        var skillNames = reactGroup.Skills.Select(s => s.Name).ToList();
        Assert.Contains("React", skillNames);
        Assert.Contains("Redux", skillNames);
        Assert.Contains("React Router", skillNames);
        Assert.Contains("React Query", skillNames);
        Assert.Contains("Next.js", skillNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_ReturnsCorrectAutoTestingSkills()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        // Link skills to groups (simulating EF Core navigation property loading)
        foreach (var group in allGroups)
        {
            group.Skills = allSkills.Where(s => s.GroupId == group.Id).ToList();
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert
        var testingGroup = result.Groups.First(g => g.Name == "Тестирование");
        var autoTestingGroup = testingGroup.ChildGroups.First(g => g.Name == "Автотестирование");

        Assert.NotEmpty(autoTestingGroup.Skills);
        var skillNames = autoTestingGroup.Skills.Select(s => s.Name).ToList();
        Assert.Contains("Selenium", skillNames);
        Assert.Contains("Cypress", skillNames);
        Assert.Contains("Playwright", skillNames);
        Assert.Contains("Appium", skillNames);
        Assert.Contains("JUnit", skillNames);
        Assert.Contains("NUnit", skillNames);
        Assert.Contains("xUnit", skillNames);
        Assert.Contains("Jest", skillNames);
        Assert.Contains("Pytest", skillNames);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_VerifySkillIdsAreCorrect()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        // Link skills to groups (simulating EF Core navigation property loading)
        foreach (var group in allGroups)
        {
            group.Skills = allSkills.Where(s => s.GroupId == group.Id).ToList();
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert - Verify C# skill has correct ID
        var csharpSkillId = Guid.Parse("20000000-0000-0000-0000-000000000001");
        var developmentGroup = result.Groups.First(g => g.Name == "Разработка");
        var backendGroup = developmentGroup.ChildGroups.First(g => g.Name == "Backend");
        var dotNetGroup = backendGroup.ChildGroups.First(g => g.Name == ".NET");
        var csharpSkill = dotNetGroup.Skills.First(s => s.Name == "C#");

        Assert.Equal(csharpSkillId, csharpSkill.Id);
    }

    [Fact]
    public async Task GetSkillsTreeAsync_WithSeedingData_VerifyGroupIdsAreCorrect()
    {
        // Arrange
        var allGroups = InterviewContextSeeding.GetAllGroups();
        var allSkills = InterviewContextSeeding.GetAllSkills();

        foreach (var skill in allSkills)
        {
            skill.Group = allGroups.FirstOrDefault(g => g.Id == skill.GroupId);
        }

        _skillGroupRepositoryMock
            .Setup(r => r.GetFullTreeAsync(Token))
            .ReturnsAsync(allGroups);

        // Act
        var result = await _skillService.GetSkillsTreeAsync(Token);

        // Assert - Verify Development group has correct ID
        var developmentGroupId = Guid.Parse("10000000-0000-0000-0000-000000000001");
        var developmentGroup = result.Groups.First(g => g.Name == "Разработка");
        
        Assert.Equal(developmentGroupId, developmentGroup.Id);
    }

    private int CountAllGroups(List<SkillGroupDto> groups)
    {
        var count = groups.Count;
        foreach (var group in groups)
        {
            count += CountAllGroups(group.ChildGroups);
        }
        return count;
    }

    private int CountAllSkills(List<SkillGroupDto> groups)
    {
        var count = groups.Sum(g => g.Skills.Count);
        foreach (var group in groups)
        {
            count += CountAllSkills(group.ChildGroups);
        }
        return count;
    }
}
