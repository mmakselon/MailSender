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
                    .Include(x=> x.EmailAccountParams)
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
                    .Include(x=>x.EmailAccountParams)
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

        public Status GetStatus(string name)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Statuses.FirstOrDefault(x => x.Name == name);
            }
        }

        public void Add(EmailMessage emailMessage)
        {
            using (var context = new ApplicationDbContext())
            {
                emailMessage.CreatedDate = DateTime.Now;
                emailMessage.AccountParamsId = emailMessage.EmailAccountParams.Id;
                emailMessage.EmailAccountParams = null;

                context.EmailMessages.Add(emailMessage);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    //logger
                    var message = e.Message;       
                }
                
            }
        }
    }
}