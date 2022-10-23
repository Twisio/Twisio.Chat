namespace Portfol.io.Chat.Domain
{
    public class AppChat
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanionId { get; set; }
        public IEnumerable<Message>? Messages { get; set; }
    }
}
