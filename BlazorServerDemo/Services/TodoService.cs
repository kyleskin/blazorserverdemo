using System.Collections;
using BlazorServerDemo.Exceptions;
using BlazorServerDemo.Models;
using BlazorServerDemo.Repositories;

namespace BlazorServerDemo.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;
    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<Todo>> GetTodosAsync()
    {
        return await _repository.GetTodosAsync();
    }

    public async Task<Todo> GetTodoAsync(TodoId id)
    {
        var todo = await _repository.GetTodoAsync(id);
        if (todo is null)
            throw new InvalidTodoException("ID does not correspond to a valid ToDo");

        return todo;
    }

    public async Task<IEnumerable<Todo>> GetIncompleteTodosAsync()
    {
        var todos = await _repository.GetTodosAsync();
        return todos.Where(t => !t.IsComplete);
    }
}