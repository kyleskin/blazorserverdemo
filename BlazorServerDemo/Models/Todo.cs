namespace BlazorServerDemo.Models;

public class Todo
{
    public TodoId  Id { get; init; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
}