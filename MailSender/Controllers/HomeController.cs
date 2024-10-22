using MailSender.Models.Domains;
using MailSender.Models.Repositories;
using MailSender.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailSender.Controllers
{
    public class HomeController : Controller
    {
        private EmailRepository _emailRepository = new EmailRepository();

        public ActionResult Index()
        {
            return View();
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