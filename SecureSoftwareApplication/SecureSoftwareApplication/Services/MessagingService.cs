using Microsoft.AspNet.Identity;
using SecureSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecureSoftwareApplication.Services
{
    public class MessagingService
    {

        /// <summary>
        /// Sends a notification to a user notifying them of the addition of jobs in their name
        /// </summary>
        /// <param name="user">the user to send the message to</param>
        public void SendJobAdditionMessage(Account user)
        {
            if (user != null)
            {

                IdentityMessage im = new IdentityMessage();

                im.Body = "A new job has been added under your account."
                    +"If this was not you, please report this to an administrator";
                im.Destination = user.PhoneNumber;
                im.Subject = "New job Added";
                SmsService sms = new SmsService();
                sms.SendAsync(im);

            }
        }
    }
}