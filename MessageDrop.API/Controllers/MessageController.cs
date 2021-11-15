using AutoMapper;
using MessageDrop.API.Model;
using MessageDrop.Core.Interface;
using MessageDrop.EF.Model;
using Microsoft.AspNetCore.Mvc;

namespace MessageDrop.API.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IMessageData _data;
        private readonly IMapper _mapper;

        public MessageController(IMessageData data, IMapper mapper)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _mapper = mapper ?? throw new ArgumentNullException();
        }

        // GET api/messages 
        [HttpGet]
        public ActionResult<IEnumerable<MessageDto>> GetMessages()
        {
            var messagesList = _data.GetAll();
            return Ok(_mapper.Map<IEnumerable<MessageDto>>(messagesList.Result));
        }

        // Get api/messages/{id}
        [HttpGet("{messageID:int}", Name = "GetMessageFromID")]
        public IActionResult GetMessageFromID(int messageID)
        {
            var messageData = _data.Get(messageID);

            if (messageData == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MessageDto>(messageData.Result));
        }

        // POST: api/messages
        [HttpPost]
        public ActionResult<MessageDto> CreateMessage(MessageCreateDto messageDto)
        {
            if (messageDto == null)
            {
                return BadRequest();
            }

            var messageModel = _mapper.Map<Message>(messageDto);

            _data.Insert(messageModel);
            _data.Save();

            var messageReadDto = _mapper.Map<MessageDto>(messageModel);

            return CreatedAtRoute(nameof(GetMessageFromID), new { messageID = messageReadDto.Id }, messageReadDto);
        }

    }
}
