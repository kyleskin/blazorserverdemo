using System.Collections;
using BlazorServerDemo.Exceptions;
using BlazorServerDemo.Models;

namespace BlazorServerDemo.Repositories;

public class TodoRepository : ITodoRepository
{
    public async Task<IEnumerable<Todo>> GetTodosAsync() => await Task.Run(() => _todos.ToList());

    public async Task<Todo?> GetTodoAsync(TodoId id) => await Task.Run(() => _todos.FirstOrDefault(t => t.Id == id));
    
    private readonly List<Todo> _todos = new()
    {
        new Todo { Title = "Prepare presentation" },
        new Todo { Title = "Commit changes" },
        new Todo { Title = "Log time" }
    };
}