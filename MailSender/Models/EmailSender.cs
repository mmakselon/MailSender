using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MailSender.Models
{
    public class EmailSender
    {
        private SmtpClient _smtp;
        private MailMessage _mail;

        private string _hostSmtp;
        private bool _enableSsl;
        private int _port;
        private string _senderEmail;
        private string _senderEmailPassword;
        private string _senderName;

        public EmailSender(EmailAccountParams emailAccountParams)
        {
            _hostSmtp = emailAccountParams.HostSmtp;
            _enableSsl = emailAccountParams.EnableSsl;
            _port = emailAccountParams.Port;
            _senderEmail = emailAccountParams.SenderEmail;
            _senderEmailPassword = emailAccountParams.SenderEmailPassword;
            _senderName = emailAccountParams.SenderName;
        }

        public async Task Send(EmailMessage emailMessage)
        {
            _mail = new MailMessage();
            _mail.From = new MailAddress(_senderEmail, _senderName);
            _mail.To.Add(new MailAddress(emailMessage.EmailTo));
            _mail.IsBodyHtml = true;
            _mail.Subject = emailMessage.Subject;
            _mail.BodyEncoding = Encoding.UTF8;
            _mail.SubjectEncoding = Encoding.UTF8;

            _mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
                emailMessage.Body, null, MediaTypeNames.Text.Html));

            _smtp = new SmtpClient
            {
                Host = _hostSmtp,
                EnableSsl = _enableSsl,
                Port = _port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_senderEmail, _senderEmailPassword)
            };

            _smtp.SendCompleted += OnSendCompleted;
            await _smtp.SendMailAsync(_mail);
        }
        private void OnSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _smtp.Dispose();
            _mail.Dispose();
        }
    }
}