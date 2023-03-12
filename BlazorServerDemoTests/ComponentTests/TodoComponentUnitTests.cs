using BlazorServerDemo.Components;
using BlazorServerDemo.Models;
using Bunit;

namespace BlazorServerDemoTests.ComponentTests;

public class TodoComponentUnitTests
{
    [Fact]
    public void InProgressTodoItem_ShouldLoadWithDoneBoxUnchecked_WhenComponentRenders()
    {
        // Arrange
        var todo = new Todo { Title = "todo test" };
        var ctx = new TestContext();

        // Act
        var cut = ctx.RenderComponent<TodoComponent>(parameters => parameters.Add(p => p.Todo, todo));
        
        // Assert
        cut.MarkupMatches("<tr><td><input type=\"checkbox\"></td><td>todo test</td></tr>");
    }
}