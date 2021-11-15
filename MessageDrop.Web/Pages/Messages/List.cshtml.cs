using MessageDrop.Core.Interface;
using MessageDrop.Core.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessageDrop.Web.Pages.Messages
{
    public class ListModel : PageModel
    {
        private readonly IMessageData _messageData;
        public IEnumerable<Message> Messages { get; set; }

        public ListModel(IMessageData messageData)
        {
            _messageData = messageData;
        }
        public async void OnGet()
        {
            Messages = await _messageData.GetAll();
        }


    }
}
