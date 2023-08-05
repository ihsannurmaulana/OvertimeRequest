namespace API.Utilities.Handlers;

public class ResponseHandler<TEntity>
{
    public int Code { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
    public TEntity? Data { get; set; }
    public string? Type { get; set; }
    public string? Title { get; set; }
    public string? TraceId { get; set; }
    public Errors? Errors { get; set; }
}

public class Errors
{
    public string[]? Email { get; set; }
    public string[]? PhoneNumber { get; set; }
    public string[]? Password { get; set; }
}