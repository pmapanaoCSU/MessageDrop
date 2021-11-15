using MessageDrop.Core.Interface;
using MessageDrop.Core.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessageDrop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMessageData _messageData;
        public IEnumerable<Message> Messages { get; set; }
        private Message message;


        public IndexModel(ILogger<IndexModel> logger, IMessageData messageData)
        {
            _logger = logger;
            _messageData = messageData;
        }

        public async void OnGet()
        {
            Messages = await _messageData.GetAll();
        }

        public void setMessage(string message)
        {
            this.message = new Message(99, message);
        }
    }
}