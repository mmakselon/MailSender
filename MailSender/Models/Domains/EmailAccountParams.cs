﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.Domains
{
    public class EmailAccountParams
    {
        public string HostSmtp { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
        public string SenderName { get; set; }
    }
}