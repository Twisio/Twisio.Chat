using AutoMapper;
using Portfol.io.Chat.Application.Common.Mappings;
using Portfol.io.Chat.Domain;

namespace Portfol.io.Chat.Application.ViewModels
{
    public class ChatLookupDto : IMapWith<AppChat>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanionId { get; set; }

        public Message? Message { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppChat, ChatLookupDto>()
                .ForMember(lookup => lookup.Id, opt => opt.MapFrom(chat => chat.Id))
                .ForMember(lookup => lookup.UserId, opt => opt.MapFrom(chat => chat.UserId))
                .ForMember(lookup => lookup.CompanionId, opt => opt.MapFrom(chat => chat.CompanionId))
                .ForMember(lookup => lookup.Message, opt => opt.MapFrom(chat => chat.Messages!.OrderBy(u => u.Date).LastOrDefault()));
        }
    }
}
