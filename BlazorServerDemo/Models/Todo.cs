namespace BlazorServerDemo.Models;

public class Todo
{
    public TodoId Id { get; } = new(new Guid());
    public required string Title { get; set; }
    public bool IsInProgress { get; private set; } = true;

    public void MarkComplete()
    {
        IsInProgress = false;
    }

    public void MarkIncomplete()
    {
        IsInProgress = true;
    }
}