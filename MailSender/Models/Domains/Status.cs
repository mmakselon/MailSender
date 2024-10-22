using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace MailSender.Models.Domains
{
    public class Status
    {
        public Status()
        {
            EmailMessages = new Collection<EmailMessage>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<EmailMessage> EmailMessages { get; set; }
    }
}