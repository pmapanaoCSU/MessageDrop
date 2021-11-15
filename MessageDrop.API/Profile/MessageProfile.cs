using MessageDrop.API.Model;
using MessageDrop.EF.Model;

namespace MessageDrop.API.Profile
{
    public class MessageProfile : AutoMapper.Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>();
            CreateMap<MessageCreateDto, Message>();
            //CreateMap<IEnumerable<MessageDto>, IEnumerable<Message>>();
            //CreateMap<IEnumerable<Message>, IEnumerable<MessageDto>>();
        }

    }
}
