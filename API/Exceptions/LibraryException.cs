namespace API.Exceptions;

public class LibraryException : Exception
{
    public int Code { get; set; }
    public string Message { get; set; }
    public LibraryException(int code, string message) : base(message)
    {
        Code = code;
    }
}
