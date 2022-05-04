﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using EEMG.Data;
using EEMG.Models;
using EEMG.Pages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EEMG.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _db;
        private IServiceProvider _serviceProvider;
        private readonly string SENDER_EMAIL = "eemgdayton@gmail.com";
        private readonly string SUPER_SECRET_PASSWORD = "1qazXSW@3edcVFR$";

        public AdminController(ApplicationDbContext context, IServiceProvider provider)
        {
            _db = context;
            _serviceProvider = provider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            AdminModel model = new AdminModel(_db);
            return RedirectToAction("/Admin", model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserToMember(string userId)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userRole = _db.UserRoles.FirstOrDefault(x => x.UserId == userId);
            var user = _db.ApplicationUsers.First(x => x.Id == userId);

            // THIS LINE IS IMPORTANT
            var oldUser = userManager.FindByIdAsync(userId);
            var oldRoleId = userRole.RoleId;
            var oldRoleName = _db.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;

            if (_db.Roles.FirstOrDefault(x => x.Id == userRole.RoleId)?.Name != "Member")
            {
                await userManager.RemoveFromRoleAsync(user, oldRoleName);
                await userManager.AddToRoleAsync(user, "Member");
                _db.Entry(user).State = EntityState.Modified;
            }

            AdminModel model = new AdminModel(_db);
            return new OkObjectResult(new { success = true, message = "" });
        }

        [HttpPost]
        public IActionResult AddUserToMailingList(string email)
        {
            MailingList ml = new MailingList() { Email = email };

            _db.MailingList.Add(ml);
            _db.SaveChanges();
            

            AdminModel model = new AdminModel(_db);
            return new OkObjectResult(new { success = true, message = "" });
        }

        [HttpPost]
        public IActionResult RemoveUserFromMailingList(string email)
        {
            var ml = _db.MailingList.FirstOrDefault(e => e.Email == email);

            if (ml != null)
            {
                _db.MailingList.Remove(ml);
                _db.SaveChanges();
            }

            AdminModel model = new AdminModel(_db);
            return new OkObjectResult(new { success = true, message = "" });
        }

        [HttpGet]
        public IActionResult SendEmail(string subject, string body, bool memberOnly, bool eventParticipantsOnly, bool entireMailingList)
        {
            try
            {
                MailMessage message = new MailMessage(SENDER_EMAIL, "codyreichard31@gmail.com", subject, body);

                var usersEmail = GetEmailList(memberOnly, eventParticipantsOnly, entireMailingList);


                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(SENDER_EMAIL, SUPER_SECRET_PASSWORD);
                client.Send(message);
            }
            catch (Exception ex)
            {
            }

            AdminModel model = new AdminModel(_db);
            return RedirectToPage("/Admin", model);
        }

        private List<string> GetEmailList(bool memberOnly, bool eventParticipantsOnly, bool entireMailingList)
        {
            if (memberOnly)
            {
                var userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                List<string> memberEmails = new List<string>();
                foreach (var user in _db.ApplicationUsers.ToList())
                {
                    var userRole = _db.UserRoles.FirstOrDefault(x => x.UserId == user.Id);
                    if (_db.Roles.FirstOrDefault(x => x.Id == userRole.RoleId)?.Name == "Member")
                        memberEmails.Add(user.Email);
                }

                return memberEmails;
            }
            else if (eventParticipantsOnly)
            {
                return _db.EventUserSignUps.Select(e => e.Email).ToList();
            }
            else if (entireMailingList)
            {
                return _db.MailingList.Select(e => e.Email).ToList();
            }
            return new List<string>();
        }

        [HttpGet]
        public IActionResult AddEvent()
        {
            return RedirectToAction("~/CreateEvent");
        }
    }
}
