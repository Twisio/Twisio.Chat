using AutoMapper;
using Portfol.io.Chat.Application.Common.Mappings;
using Portfol.io.Chat.Domain;

namespace Portfol.io.Chat.Application.ViewModels
{
    public class ChatDto : IMapWith<AppChat>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanionId { get; set; }

        public IEnumerable<Message>? Messages { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppChat, ChatDto>()
                .ForMember(lookup => lookup.Id, opt => opt.MapFrom(chat => chat.Id))
                .ForMember(lookup => lookup.UserId, opt => opt.MapFrom(chat => chat.UserId))
                .ForMember(lookup => lookup.CompanionId, opt => opt.MapFrom(chat => chat.CompanionId))
                .ForMember(lookup => lookup.Messages, opt => opt.MapFrom(chat => chat.Messages));
        }
    }
}
