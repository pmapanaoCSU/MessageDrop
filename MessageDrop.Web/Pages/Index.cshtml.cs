using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessageDrop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<String> messages;
        public String message;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            messages = new List<String>();
            messages.Add("Cool messages");
            messages.Add("wow way 2 go");
            messages.Add("ur so cool");
        }

        public void OnGet()
        {
            message = messages[messages.Count - 1];
        }
    }
}