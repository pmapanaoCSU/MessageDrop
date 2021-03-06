using MessageDrop.Core.Interface;
using MessageDrop.Core.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessageDrop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        /*Variables Eric added, may not be correct*/
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

        public String SetMessage(string message)
        {
            this.message = new Message(0, message);
            _messageData.Insert(this.message);
            OnGet();
            return this.message.MessageString;
        }

    }
}