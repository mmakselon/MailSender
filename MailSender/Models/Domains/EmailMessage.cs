using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MailSender.Models.Domains
{
    public class EmailMessage
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("E-mail nadawcy")]
        public string EmailFrom { get; set; }

        [Required]
        [DisplayName("E-mail odbiorcy")]
        public string EmailTo { get; set; }

        [DisplayName("DW")]
        public string CcField { get; set; }

        [DisplayName("UDW")]
        public string BccField { get; set; }

        [Required]
        [DisplayName("Temat")]
        public string Subject { get; set; }

        [Required]
        [DisplayName("Treść wiadomości")]
        public string Body { get; set; }

        [DisplayName("Odpowiedz do")]
        public string ReplayTo { get; set; }

        [DisplayName("Data utworzenia")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Data wysyłki")]
        public DateTime? SendDate { get; set; }

        [DisplayName("Status")]
        public int StatusId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }


        public Status Status { get; set; }
        public ApplicationUser User { get; set; }

    }
}