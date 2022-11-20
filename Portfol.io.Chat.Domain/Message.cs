using System.Text.Json.Serialization;

namespace Portfol.io.Chat.Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }

        [JsonIgnore]
        public AppChat Chat { get; set; } = null!;
    }
}