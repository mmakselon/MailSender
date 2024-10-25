using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.Repositories
{
    public class EmailAccountParamsRepository
    {
        public List<EmailAccountParams> GetEmailParameters(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.EmailAccountsParameters.Where(x => x.UserId == userId).ToList();
            }
        }

        public EmailAccountParams GetEmailParameters(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.EmailAccountsParameters.Single(x => x.Id == id && x.UserId == userId);
            }
        }

        public void AddNewAccount(EmailAccountParams emailAccountParameters)
        {
            using (var context = new ApplicationDbContext())
            {
                context.EmailAccountsParameters.Add(emailAccountParameters);
                context.SaveChanges();
            }
        }

        public void UpdateAccount(EmailAccountParams emailAccountParameters)
        {
            using (var context = new ApplicationDbContext())
            {
                var emailParametersToUpdate = context.EmailAccountsParameters
                     .Single(x => x.Id == emailAccountParameters.Id && x.UserId == emailAccountParameters.UserId);

                emailParametersToUpdate.Port = emailAccountParameters.Port;
                emailParametersToUpdate.HostSmtp = emailAccountParameters.HostSmtp;
                emailParametersToUpdate.SenderEmail = emailAccountParameters.SenderEmail;
                emailParametersToUpdate.SenderEmailPassword = emailAccountParameters.SenderEmailPassword;
                emailParametersToUpdate.SenderName = emailAccountParameters.SenderName;
                emailParametersToUpdate.EnableSsl = emailAccountParameters.EnableSsl;

                context.SaveChanges();
            }
        }

        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var accountToDelete = context.EmailAccountsParameters.Single(x => x.Id == id && x.UserId == userId);
                context.EmailAccountsParameters.Remove(accountToDelete);
                context.SaveChanges();
            }
        }
    }
}