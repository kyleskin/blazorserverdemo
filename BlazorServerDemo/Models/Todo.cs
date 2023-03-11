namespace BlazorServerDemo.Models;

public class Todo
{
    public TodoId Id { get; init; } = new TodoId(new Guid());
    public required string Title { get; set; }
    public bool IsDone { get; set; } = false;
}