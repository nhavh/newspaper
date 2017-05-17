using System;
using System.Net.Configuration;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web;
using System.IO;

namespace TMV.Utilities
{
    public class MailHelper
    {
        private delegate void SendEmailDelegate(MailMessage m);

        private static void SendEmailResponse(IAsyncResult ar)
        {
            var sd = (SendEmailDelegate)(ar.AsyncState);

            sd.EndInvoke(ar);
        }

        public static bool SendEmailDirect(string to, string bcc, string cc, string subject, string body)
        {
            try
            {
                var smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                // Initialize the mailmessage class
                var message = new MailMessage();
                message.From = new MailAddress(smtpSec.Network.UserName,
                                               ConfigurationManager.AppSettings["EmailDisplayName"]);
                // Set the recepient address of the mail message
                if (!string.IsNullOrEmpty(to))
                {
                    to = to.Replace(";", ",");
                    if (to.Contains(","))
                    {
                        var sto = to.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var s in sto)
                        {
                            if (HtmlHelper.ValidateEmail(s))
                                message.To.Add(new MailAddress(s));
                        }
                    }
                    else
                    {
                        if (HtmlHelper.ValidateEmail(to))
                            message.To.Add(new MailAddress(to));
                    }
                }

                // Check if the bcc value is null or an empty string
                if (!string.IsNullOrEmpty(bcc))
                {
                    bcc = bcc.Replace(";", ",");
                    if (bcc.Contains(","))
                    {
                        var sbcc = bcc.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var s in sbcc)
                        {
                            if (HtmlHelper.ValidateEmail(s))
                                message.Bcc.Add(new MailAddress(s));
                        }
                    }
                    else
                    {
                        if (HtmlHelper.ValidateEmail(bcc))
                            message.Bcc.Add(new MailAddress(bcc));
                    }
                }
                // Check if the cc value is null or an empty value
                if (!string.IsNullOrEmpty(cc))
                {
                    cc = cc.Replace(";", ",");
                    if (cc.Contains(","))
                    {
                        var scc = cc.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var s in scc)
                        {
                            if (HtmlHelper.ValidateEmail(s))
                                message.CC.Add(new MailAddress(s));
                        }
                    }
                    else
                    {
                        if (HtmlHelper.ValidateEmail(cc))
                            message.CC.Add(new MailAddress(cc));
                    }
                }

                message.Subject = subject;
                // Set the body of the mail message
                message.Body = body;

                message.IsBodyHtml = true;

                // Set the priority of the mail message to normal
                message.Priority = MailPriority.Normal;
                var encode = new UTF8Encoding();
                message.BodyEncoding = encode;

                /* Create SMTP Client and add credentials */
                var smtpClient = new SmtpClient(smtpSec.Network.Host)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true, //Open SSL when GoogleMail, YahooMail
                    Credentials = new NetworkCredential(smtpSec.Network.UserName, smtpSec.Network.Password)
                };
                /* Email with Authentication */

                /* Send the message */
                //smtpClient.Send(message);
                var sd = new SendEmailDelegate(smtpClient.Send);
                var cb = new AsyncCallback(SendEmailResponse);
                sd.BeginInvoke(message, cb, sd);
                return true;
            }
            catch (Exception ex)
            {
                //Exceptions.Logger.Error(ex);
                return false;
            }
        }

        public static void SendEmailAttach(string to, string bcc, string subject, string body, HttpPostedFile attachment)
        {
            try
            {
                var smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                // Initialize the mailmessage class
                var message = new MailMessage();
                message.From = new MailAddress(smtpSec.Network.UserName,
                                               ConfigurationManager.AppSettings["EmailDisplayName"]);
                // Set the recepient address of the mail message
                if (!string.IsNullOrEmpty(to))
                {
                    if (to.Contains(","))
                    {
                        var sto = to.Split(new[] { ',' });
                        for (var i = 0; i < sto.Length; i++)
                        {
                            if (sto[i] != null && sto[i] != string.Empty)
                                message.To.Add(new MailAddress(sto[i]));
                        }
                    }
                    else
                    {
                        message.To.Add(new MailAddress(to));
                    }
                }

                // Check if the bcc value is null or an empty string
                if (!string.IsNullOrEmpty(bcc))
                {
                    if (bcc.Contains(","))
                    {
                        var sbcc = bcc.Split(new char[] { ',' });
                        for (var i = 0; i < sbcc.Length; i++)
                        {
                            if (sbcc[i] != null && sbcc[i] != string.Empty)
                                message.Bcc.Add(new MailAddress(sbcc[i]));
                        }
                    }
                    else
                    {
                        // Set the Bcc address of the mail message
                        message.Bcc.Add(new MailAddress(bcc));
                    }
                }

                message.Subject = subject;
                // Set the body of the mail message
                message.Body = body;

                message.IsBodyHtml = true;

                // Set the priority of the mail message to normal
                message.Priority = MailPriority.Normal;
                if (attachment != null && attachment.ContentLength != 0)
                    message.Attachments.Add(new Attachment(attachment.InputStream, attachment.FileName));
                var encode = new UTF8Encoding();
                message.BodyEncoding = encode;

                /* Create SMTP Client and add credentials */
                var smtpClient = new SmtpClient(smtpSec.Network.Host)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(smtpSec.Network.UserName, smtpSec.Network.Password)
                };
                /* Email with Authentication */

                /* Send the message */
                //smtpClient.Send(message);
                var sd = new SendEmailDelegate(smtpClient.Send);
                var cb = new AsyncCallback(SendEmailResponse);
                sd.BeginInvoke(message, cb, sd);
            }
            catch (Exception ex)
            {
                //Exceptions.Logger.Error(ex);
            }
        }

        public static bool ReadEmailTemplate(string path, ref string subject, ref string body)
        {
            var result = string.Empty;
            var filePath = HttpContext.Current.Server.MapPath(path);

            if (!File.Exists(filePath)) return false;
            var lines = File.ReadAllLines(filePath);
            if (lines.Length <= 0) return false;
            subject = lines[0];
            int i;
            for (i = 1; i < lines.Length; i++)
            {
                body += lines[i];
            }
            return true;
        }
    }
}
