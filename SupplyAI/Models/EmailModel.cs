using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MI3.Models
{
    public class EmailModel
    {
        public MailMessage Message { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get { return true; } }
        public MailAddress Recipient { get; set; }

        public EmailModel(string recipient)
        {
            Recipient = new MailAddress(recipient);
            Message = new MailMessage();
        }

        public void MakeAbstractReviewedEmail(bool approved, string rationale)
        {
            Subject = "Your abstract has been reviewed";
            string bodyHeader = "<p>Hello " + Recipient.ToString() + ",</p>";
            string approveOrReject = approved ? "approved" : "denied";
            string feedback = rationale != null ? string.Format(
                "They have provided the following feedback:</p>{0}",
                "<p><em>" + rationale + "</em></p>") : "";
            string closing = approved ? "<p>Please log back into your account and " +
                "return to the 'Upload Dataset' page to complete the upload process.</p>" :
                "<p>You can submit another abstract by logging back in and returning to the 'Upload Dataset' page.</p>";
            string signature = "<p>Thank you.</p><p>-The MI3 Staff</p>";
            Body = string.Format(bodyHeader + "<p>One of our administrators has reviewed your abstract, and your " +
                "submission request has been {0}.  {1} {2}" + signature, approveOrReject, feedback, closing);          
        }

        public async Task Send()
        {
            Message.To.Add(Recipient);
            Message.Subject = Subject;
            Message.Body = Body;
            Message.IsBodyHtml = IsBodyHtml;
            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(Message);
            }
        }
    }
}