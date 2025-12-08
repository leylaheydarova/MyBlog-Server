namespace MyBlog.App.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message) : base($"Sorry, {message} not found!")
        {
        }
    }
}
