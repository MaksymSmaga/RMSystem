using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RMSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string Message { get; set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
 
        // Метод передачи в Index предстваление. 
        public void OnGet()
        {
            Message = $"Risk Management System!";
        }
    }
}
