using System.Collections;
using BlazorServerDemo.Models;

namespace BlazorServerDemo.Services;

public interface ITodoService
{
    Task<IEnumerable<Todo>> GetTodosAsync();
    Task<Todo> GetTodoAsync(TodoId id);
    Task<IEnumerable<Todo>> GetIncompleteTodosAsync();
}