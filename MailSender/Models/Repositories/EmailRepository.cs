using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace MailSender.Models.Repositories
{
    public class EmailRepository
    {
        public List<EmailMessage> GetEmailMessages(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.EmailMessages
                    .Include(x => x.Status)
                    .Where(x => x.UserId == userId)
                    .ToList();
            }
        }

        public EmailMessage GetEmailMessage(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.EmailMessages
                    .Include(x => x.Status)
                    .Single(x => x.Id == id && x.UserId == userId);
            }
        }

        public List<Status> GetStatuses()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Statuses.ToList();
            }
        }

        public void Add(EmailMessage emailMessage)
        {
            using (var context = new ApplicationDbContext())
            {
                emailMessage.CreatedDate = DateTime.Now;
                context.EmailMessages.Add(emailMessage);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    var message = e.Message;       
                }
                
            }
        }
    }
}