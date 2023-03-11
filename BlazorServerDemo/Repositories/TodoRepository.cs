using BlazorServerDemo.Exceptions;
using BlazorServerDemo.Models;

namespace BlazorServerDemo.Repositories;

public class TodoRepository : ITodoRepository
{
    public async Task<IEnumerable<Todo>> GetTodosAsync() => await Task.Run(() => _todos.ToList());

    public async Task<Todo?> GetTodoAsync(TodoId id) => await Task.Run(() => _todos.FirstOrDefault(t => t.Id == id));

    private readonly List<Todo> _todos = new()
    {
        new Todo { Id = new TodoId(new Guid()), Title = "Prepare presentation", IsDone = false },
        new Todo { Id = new TodoId(new Guid()), Title = "Commit changes", IsDone = false },
        new Todo { Id = new TodoId(new Guid()), Title = "Log time", IsDone = false }
    };
}