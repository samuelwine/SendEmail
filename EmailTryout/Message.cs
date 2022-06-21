using MimeKit;

namespace EmailTryout
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; } = new();
        public string Subject { get; set; }
        public string Content { get; set; }

        public Message(IEnumerable<Recipient> recipients, string subject, string content)
        {
            To.AddRange(recipients.Select(x => new MailboxAddress(x.Name, x.Address)));
            Subject = subject;
            Content = content;
        }
    }
}
