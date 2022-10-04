namespace Portfol.io.Chat.Models
{
    public class AppChat
    {
        public Guid Id{ get; set; }
        public IEnumerable<Guid>? UserIds { get; set; }

        public IEnumerable<Message>? Messages { get; set; }
    }
}
