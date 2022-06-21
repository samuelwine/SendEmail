using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmailTryout.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAppEmailSender _emailSender;

        public IndexModel(ILogger<IndexModel> logger, IAppEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        public void OnGet()
        {
            var recipients = new List<Recipient>
            {
                new Recipient
                {
                    Address = "samuelwine@gmail.com",
                    Name = "Shmuel Yaakov"
                },
                new Recipient
                {
                    Name = "Shmuel Yaakov Next",
                    Address = "sywnext@gmail.com"
                }
            };
            var message = new Message(recipients, "Not Just Testing", "This is the content!!!!");
            _emailSender.SendEmail(message);
        }
    }
}