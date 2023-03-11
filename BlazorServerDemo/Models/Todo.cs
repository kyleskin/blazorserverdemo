namespace BlazorServerDemo.Models;

public class Todo
{
    public TodoId Id { get; init; } = new TodoId(new Guid());
    public required string Title { get; set; }
    public bool IsComplete { get; private set; } = false;

    public void MarkComplete()
    {
        IsComplete = true;
    }

    public void MarkIncomplete()
    {
        IsComplete = false;
    }
}