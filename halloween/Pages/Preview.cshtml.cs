using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using halloween.Model;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

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
        public PreviewModel(DB myDB,IConfiguration myConfiguration)
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
        public string Message { get; set;}
        public IActionResult OnPost(int id = 0) {
            if (id > 0) {
                bridgeGreetings = _myDB.Greetings.Find(id);

                try
                {
                    // SEND 
                    MailMessage Mailer = new MailMessage();

                    Mailer.To.Add(new MailAddress(bridgeGreetings.recipientemail, bridgeGreetings.recipientname));
                    Mailer.Subject = bridgeGreetings.subject;

                    Mailer.Body = "<img src='http://meghann.wowoco.org/images/thumbnail.jpg'/>"
                        + bridgeGreetings.mesgfromuser;
                       /* Mailer.fromName + "has a greeting for you! Visit http://meghann.wowoco.org/read/" + Contact.ID*/


                    Mailer.Body = bridgeGreetings.mesgfromuser;
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
                    Message = "Yikes, we were unable to send your egreeting."; 
                }
            }
            return Page();
        }
        


    }
}

