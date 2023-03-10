using System.Collections;
using BlazorServerDemo.Models;

namespace BlazorServerDemo.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>> GetTodosAsync();
    Task<Todo?> GetTodoAsync(TodoId id);
}