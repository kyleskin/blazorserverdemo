namespace BlazorServerDemo.Exceptions;

public class InvalidTodoException : Exception
{
    public InvalidTodoException()
    {
    }

    public InvalidTodoException(string message)
        : base(message)
    {
    }

    public InvalidTodoException(string message, Exception inner)
        : base(message, inner)
    {
    }
}