﻿using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.ViewModels
{
    public class EditEmailMessageViewModel
    {

        public EmailMessage EmailMessage { get; set; }
        public List<Status> Statuses { get; set; }

        public string Heading { get; set; }
    }
}