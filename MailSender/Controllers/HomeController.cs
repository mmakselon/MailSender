using MailSender.Models.Domains;
using MailSender.Models.Repositories;
using MailSender.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MailSender.Controllers
{
    public class HomeController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();
        private EmailAccountParamsRepository _emailAccountParamsRepository = new EmailAccountParamsRepository();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var emailMessages = _emailRepository.GetEmailMessages(userId);

            return View(emailMessages);
        }


        public ActionResult EmailMessage(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var email = id == 0 ? GetNewEmail(userId) :
                _emailRepository.GetEmailMessage(id, userId);

            var vm = PrepareEmailVm(email, userId);

            return View(vm);
        }

        private EditEmailMessageViewModel PrepareEmailVm(
            EmailMessage email, string userId)
        {
            return new EditEmailMessageViewModel
            {
                EmailMessage = email,
                Heading = email.Id == 0 ? "Dodawanie nowego E-maila" :
                "E-mail",
                Statuses = _emailRepository.GetStatuses()
            };
        }

        private EmailMessage GetNewEmail(string userId)
        {
            return new EmailMessage
            {
                UserId = userId,
                CreatedDate = DateTime.Now
            };
        }

        public ActionResult Accounts()
        {
            var userId = User.Identity.GetUserId();
            var emailParameters = _emailAccountParamsRepository.GetEmailParameters(userId);
            return View(emailParameters);
        }

        public ActionResult EmailAccountParams(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            var emailAccountParams = id == 0 ?
                AddEmailParams(userId) : _emailAccountParamsRepository.GetEmailParameters(id, userId);


            return View(emailAccountParams);
        }

        private EmailAccountParams AddEmailParams(string userId)
        {
            return new EmailAccountParams
            {
                UserId = userId
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmailAccountParams(EmailAccountParams emailAccountParams)
        {
            var userId = User.Identity.GetUserId();
            emailAccountParams.UserId = userId;

            if (emailAccountParams.Id == 0)
                _emailAccountParamsRepository.AddNewAccount(emailAccountParams);
            else
                _emailAccountParamsRepository.UpdateAccount(emailAccountParams);

            return RedirectToAction("Accounts");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmailMessage(EmailMessage emailMessage)
        {
            var userId = User.Identity.GetUserId();
            emailMessage.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = PrepareEmailVm(emailMessage, userId);
                return View("EmailMessage", vm);
            }

            if (emailMessage.Id == 0)
                _emailRepository.Add(emailMessage);
            //else //TO DO
                //_emailRepository.Update(emailMessage);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                //_invoiceRepository.Delete(id, userId);
            }
            catch (Exception exception)
            {
                //logowanie do pliku
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}