using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Gaz.Models.Models;

namespace GazProjec.Models
{
    public class MailingService : IDisposable
    {
        private readonly string IMPERSONATION_MAIL_FROM = "donotreply@gazproject.com";
        private SmtpClient _smtpClient;

        public MailingService()
        {
        }

        /// <summary>
        /// sends mail with smtp defined values
        /// </summary>
        private bool SendMail(string from, string subject, string body, params string[] recipients)
        {
            using (var mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(@from);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                foreach (var recip in recipients)
                    mailMessage.To.Add(new MailAddress(recip));

                if (_smtpClient == null) 
                    _smtpClient = new SmtpClient();

                try
                {
                    _smtpClient.Send(mailMessage);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// send user a complaint read notification via mail
        /// </summary>
        public bool SendMail_UserComplaintRead(UserComplaint complaint)
        {
            if (complaint.User == null) return false;

            //user information
            var fullName = string.Format("{0} {1}", complaint.User.FirstName, complaint.User.LastName);
            var userEmail = complaint.User.Email;

            //building message
            const string subject = "update from gaz company";
            var body =
            string.Format("<table><tr><td>hi {0},</td></tr><tr><td>your complaint (#{1}) is being addressed.</td></tr></table>", fullName, complaint.ID);

            //sending message
            return this.SendMail(this.IMPERSONATION_MAIL_FROM, subject, body, userEmail);
        }



        /// <summary>
        /// dispose smtpclient
        /// </summary>
        public void Dispose()
        {
            if (_smtpClient != null)
            {
                _smtpClient.Dispose();
            }
        }
    }
}