using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using halloween.Model;
using System.Net.Mail;

namespace halloween.Pages
{
    public class PreviewModel : PageModel
    {
        // BRIDGE TO GREETINGS MODEL
        [BindProperty]
        public Greetings bridgeGreetings { get; set; }

        // HEY, CONNECT MY DATABASE TO THIS MODEL
        private DB _myDB;
        public PreviewModel(DB myDB)
        {
            _myDB = myDB;
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
                    Mailer.Body = bridgeGreetings.mesgfromuser;
                    Mailer.From = new MailAddress(bridgeGreetings.sendersemail, bridgeGreetings.sendersname);

                    Mailer.IsBodyHtml = true;

                    using (SmtpClient smtpServer = new SmtpClient())
                    {

                        smtpServer.EnableSsl = true;
                        smtpServer.Host = "smtp.wowoco.org";//CHANGE DEPENDING ON YOUR MAIL SERVER
                        smtpServer.Port = 25;//CHANGE DEPENDING ON YOUR MAIL SERVER
                        smtpServer.UseDefaultCredentials = false;
                        smtpServer.Send(Mailer);

                    }
                    //DB-RELATED: ASSIGN SEND INFO TO DATABASE
                    bridgeGreetings.sendDate = DateTime.Now.ToString();
                    bridgeGreetings.sendIP = this.HttpContext.Connection.RemoteIpAddress.ToString();


                    //DB-RELATED: UPDATE REOD ON THE DATABASE
                    _myDB.Greetings.Update(bridgeGreetings);
                    _myDB.SaveChanges();

                    return RedirectToPage("Complete");

                }
                catch
                {
                    Message = "Sorry, Sandra made it bomb!" 
                }
            }
            return Page();
        }
        


    }
}