namespace Portfol.io.Chat.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }

        public AppChat Chat { get; set; } = null!;
    }
}