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
    private readonly IRenderedComponent<TodoList> _page;
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

        _mockTodos.Setup(s => s.GetTodosAsync()).ReturnsAsync(_todos);

        _testContext.Services.AddSingleton(_mockTodos.Object);
        _page = _testContext.RenderComponent<TodoList>();
    }

    public void Dispose()
    {
        _page.Dispose();
        _testContext.Dispose();
    }

    [Fact]
    public void PageInstance_ShouldLoadAllTodos_WhenPageLoaded()
    {
        var todos = _page.Instance.Todos;

        todos.Should().BeEquivalentTo(_todos);
    }

    [Fact]
    public async void ShowIncompleteTodos_ShouldLoadOnlyIncompleteViews_WhenCalled()
    {
        _mockTodos.Setup(t => t.GetIncompleteTodosAsync())
            .ReturnsAsync(_todos.Where(t => !t.IsComplete));
        
        await _page.Instance.ShowInCompleteTodos();
        var todos = _page.Instance.Todos;

        todos.Should().NotBeEmpty()
            .And.NotContain(t => t.IsComplete);
    }
}