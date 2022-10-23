namespace Portfol.io.Chat.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" with {key} not found.") { }
    }
}
