using BlazorServerDemo.Components;
using BlazorServerDemo.Models;
using BlazorServerDemo.Pages;
using BlazorServerDemo.Services;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorServerDemoTests.PageTests;

public class TodoListPageUnitTests
{
    private readonly List<Todo> _todos = new()
    {
        new() { Title = "Prepare presentation" },
        new() { Title = "Commit changes" },
        new() { Title = "Log time" }
    };
    
    public TodoListPageUnitTests()
    {
        _todos[0].MarkComplete();
    }

    [Fact]
    public void PageInstance_ShouldLoadOnlyInProgressTodos_WhenPageLoaded()
    {
        // Arrange
        var mockTodos = new Mock<ITodoService>();
        mockTodos.Setup(t => t.GetInProgressTodosAsync())
                 .ReturnsAsync(_todos.Where(t => t.IsInProgress));
        
        using var ctx = new TestContext();
        ctx.Services.AddSingleton(mockTodos.Object);
        var cut = ctx.RenderComponent<TodoList>();
        
        // Act
        var todos = cut.Instance.Todos;

        // Assert
        mockTodos.Verify(x => x.GetInProgressTodosAsync(), Times.Once);
        todos.Should().NotBeEmpty()
             .And.NotContain(t => !t.IsInProgress);
    }

    [Fact]
    public async void ShowAllTodos_ShouldLoadAllTodos_WhenCalled()
    {
        // Arrange
        var mockTodos = new Mock<ITodoService>();
        mockTodos.Setup(t => t.GetTodosAsync())
                 .ReturnsAsync(_todos);
        
        using var ctx = new TestContext();
        ctx.Services.AddSingleton(mockTodos.Object);
        var cut = ctx.RenderComponent<TodoList>();
        
        // Act
        await cut.Instance.ShowAllTodos();
        var todos = cut.Instance.Todos;

        // Assert
        mockTodos.Verify(m => m.GetTodosAsync(), Times.Once);
        todos.Should().BeEquivalentTo(_todos);
    }

    [Fact]
    public void PageView_ShouldHaveTableWithInProgressTodos_WhenPageLoads()
    {
        // Arrange
        var mockTodos = new Mock<ITodoService>();
        mockTodos.Setup(t => t.GetInProgressTodosAsync())
            .ReturnsAsync(_todos.Where(t => t.IsInProgress));

        using var ctx = new TestContext();
        ctx.Services.AddSingleton(mockTodos.Object);
        var cut = ctx.RenderComponent<TodoList>();

        // Act
        var todos = cut.FindComponents<TodoComponent>();

        // Assert
        todos.Should().HaveCount(2);

    }
    
    [Fact]
    public void ShowAllTodosButton_ShouldRender_WhenPageLoads()
    {
        // Arrange
        var mockTodos = new Mock<ITodoService>();
        mockTodos.Setup(t => t.GetTodosAsync())
            .ReturnsAsync(_todos);
        
        using var ctx = new TestContext();
        ctx.Services.AddSingleton(mockTodos.Object);
        var cut = ctx.RenderComponent<TodoList>();

        // Act
        var button = cut.Find("button");

        // Assert
        button.InnerHtml.Should().Be("Show All Todos");
        button.MarkupMatches("<button>Show All Todos</button>");
    }

    [Fact]
    public void OnShowAllTodosClicked_ShouldDisplayAllTodos_WhenClicked()
    {
        // Arrange
        var mockTodos = new Mock<ITodoService>();
        mockTodos.Setup(t => t.GetTodosAsync())
            .ReturnsAsync(_todos);

        using var ctx = new TestContext();
        ctx.Services.AddSingleton(mockTodos.Object);
        var cut = ctx.RenderComponent<TodoList>();

        // Act
        cut.Find("button").Click();
        var todos = cut.FindComponents<TodoComponent>();
        
        // Assert
        mockTodos.Verify(m => m.GetTodosAsync(), Times.Once);
        todos.Should().HaveCount(3);
    }
}