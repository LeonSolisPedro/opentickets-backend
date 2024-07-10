
namespace Core.Dto;

public class Response
{
    public bool Success { get; set; }

    public string Message { get; set; } = "Completed";

    public object? Data { get; set; }
}