using BlazorServerDemo.Exceptions;
using BlazorServerDemo.Models;
using BlazorServerDemo.Repositories;
using BlazorServerDemo.Services;
using FluentAssertions;

namespace BlazorServerDemoTests.ServiceTests;

public class TodoServiceUnitTests
{
    private readonly List<Todo> _todos = new()
    {
        new() { Title = "Prepare presentation" },
        new() { Title = "Commit changes" },
        new() { Title = "Log time" }
    };

    public TodoServiceUnitTests()
    {
        _todos[0].MarkComplete();
    }
    
    [Fact]
    public async void GetTodosAsync_ShouldReturnAllTodos_WhenCalled()
    {
        // Arrange
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(m => m.GetTodosAsync()).ReturnsAsync(_todos);
        var sut = new TodoService(mockRepo.Object);

        // Act
        var todos = await sut.GetTodosAsync();

        // Assert
        mockRepo.Verify(m => m.GetTodosAsync(), Times.Once);
        todos.Should().BeEquivalentTo(_todos);
    }

    [Fact]
    public async void GetTodo_ShouldReturnASingleTodo_WhenValidTodoIdIsUsed()
    {
        // Arrange
        var todoId = _todos[0].Id;
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(m => m.GetTodoAsync(todoId)).ReturnsAsync(_todos[0]);
        var sut = new TodoService(mockRepo.Object);

        // Act
        var todo = await sut.GetTodoAsync(todoId);

        // Assert
        todo.Should().BeEquivalentTo(_todos[0]);
        mockRepo.Verify(m => m.GetTodoAsync(todoId), Times.Once);
    }

    [Fact]
    public async void GetTodo_ShouldThrowInvalidTodoException_WhenInvalidIdIsUsed()
    {
        // Arrange
        var todoId = new TodoId(new Guid());
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(m => m.GetTodoAsync(It.IsAny<TodoId>())).ReturnsAsync((Todo)null);
        var sut = new TodoService(mockRepo.Object);
        
        // Act
        Func<Task> action = () => sut.GetTodoAsync(new TodoId(new Guid()));

        // Assert
        await action.Should().ThrowAsync<InvalidTodoException>();
    }

    [Fact]
    public async void GetInProgressTodos_ShouldReturnOnlyInProgressTodos_WhenCalled()
    {
        // Arrange
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(m => m.GetTodosAsync()).ReturnsAsync(_todos);
        var sut = new TodoService(mockRepo.Object);
        
        // Act
        var todos = await sut.GetInProgressTodosAsync();

        // Assert
        todos.Should().NotBeEmpty()
            .And.HaveCount(2)
            .And.NotContain(t => !t.IsInProgress);
    }
}