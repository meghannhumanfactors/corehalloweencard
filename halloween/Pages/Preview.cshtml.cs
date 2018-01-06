using halloween.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace halloween.Pages
{
    public class PreviewModel : PageModel
    {
        // BRIDGE TO GREETINGS MODEL
        [BindProperty]
        public Greetings bridgeGreetings { get; set; }

        private IConfiguration _myConfiguration { get; set; }

        // HEY, CONNECT MY DATABASE TO THIS MODEL
        private DB _myDB;
        public PreviewModel(DB myDB, IConfiguration myConfiguration)
        {
            _myDB = myDB;
            _myConfiguration = myConfiguration;
        }


        public void OnGet(int ID = 0)
        {

            if (ID > 0)
            {

                bridgeGreetings = _myDB.Greetings.Find(ID);
            }
        }

        // EMAIL-RELATED
        public string Message { get; set; }
        public IActionResult OnPost(int id = 0)
        {
            if (id > 0)
            {
                bridgeGreetings = _myDB.Greetings.Find(id);
                var emailUrl = "http://meghann.wowoco.org/email?id=" + bridgeGreetings.ID;

                try
                {
                    // SEND 
                    MailMessage Mailer = new MailMessage();

                    Mailer.To.Add(new MailAddress(bridgeGreetings.recipientemail, bridgeGreetings.recipientname));
                    Mailer.Subject = bridgeGreetings.subject;


                    Mailer.Body = "You have a message from " + bridgeGreetings.sendersemail + "(" + bridgeGreetings.sendersname + ").  Please visit http://meghann.wowoco.org/read/" + bridgeGreetings.ID;

                    using (WebClient client = new WebClient())
                    {
                        Mailer.Body = client.DownloadString(new Uri(emailUrl));
                    }

                    Mailer.From = new MailAddress(bridgeGreetings.sendersemail, bridgeGreetings.sendersname);

                    Mailer.IsBodyHtml = true;

                    using (SmtpClient smtpServer = new SmtpClient())
                    {

                        smtpServer.EnableSsl = Boolean.Parse(_myConfiguration["SMTP:EnableSsl"]);
                        smtpServer.Host = _myConfiguration["SMTP: Host"];//CHANGE DEPENDING ON YOUR MAIL SERVER
                        smtpServer.Port = Int32.Parse(_myConfiguration["SMTP:Port"]); //CHANGE DEPENDING ON YOUR MAIL SERVER
                        smtpServer.UseDefaultCredentials = Boolean.Parse(_myConfiguration["SMTP:UseDefaultCredentials"]);
                        smtpServer.Send(Mailer);

                    }


                    //DB-RELATED: ASSIGN SEND INFO TO DATABASE
                    bridgeGreetings.sendDate = DateTime.Now.ToString();
                    bridgeGreetings.sendIP = this.HttpContext.Connection.RemoteIpAddress.ToString();


                    //DB-RELATED: UPDATE RECORD ON THE DATABASE
                    _myDB.Greetings.Update(bridgeGreetings);
                    _myDB.SaveChanges();

                    return RedirectToPage("Complete", new { ID = bridgeGreetings.ID });

                }
                catch
                {
                    Message = "Yikes, we were unable to send your eGreeting.";
                }
            }
            return Page();
        }



    }
}

