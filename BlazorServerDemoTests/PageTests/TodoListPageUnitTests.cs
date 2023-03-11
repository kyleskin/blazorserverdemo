using BlazorServerDemo.Models;
using BlazorServerDemo.Pages;
using BlazorServerDemo.Services;
using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorServerDemoTests.PageTests;

public class TodoListPageUnitTests : IDisposable
{
    private readonly TestContext _testContext;
    private Mock<ITodoService> _mockTodos;
    private readonly List<Todo> _todos = new()
    {
        new() { Title = "Prepare presentation" },
        new() { Title = "Commit changes" },
        new() { Title = "Log time" }
    };

    public TodoListPageUnitTests()
    {
        _testContext = new TestContext();
        _mockTodos = new();
        _todos[0].MarkComplete();
        _testContext.Services.AddSingleton(_mockTodos.Object);
    }

    public void Dispose()
    {
        _testContext.Dispose();
    }

    [Fact]
    public void PageInstance_ShouldLoadOnlyIncompleteTodos_WhenPageLoaded()
    {
        _mockTodos.Setup(t => t.GetIncompleteTodosAsync())
            .ReturnsAsync(_todos.Where(t => !t.IsComplete));
        var page = _testContext.RenderComponent<TodoList>();
        
        var todos = page.Instance.Todos;

        _mockTodos.Verify(x => x.GetIncompleteTodosAsync(), Times.Once);
        todos.Should().NotBeEmpty()
            .And.NotContain(t => t.IsComplete);
    }

    [Fact]
    public async void ShowAllTodos_ShouldLoadAllTodos_WhenCalled()
    {
        _mockTodos.Setup(t => t.GetTodosAsync())
            .ReturnsAsync(_todos);
        var page = _testContext.RenderComponent<TodoList>();
        
        await page.Instance.ShowAllTodos();
        var todos = page.Instance.Todos;

        _mockTodos.Verify(m => m.GetTodosAsync(), Times.Once);
        todos.Should().BeEquivalentTo(_todos);
    }
}