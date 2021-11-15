using MessageDrop.Core.Interface;
using MessageDrop.Core.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessageDrop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMessageData _messageData;
        public IEnumerable<Message> messages { get; set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}