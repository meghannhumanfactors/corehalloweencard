using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using halloween.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace halloween.Pages
{
    public class IndexModel : PageModel
    {
        // DEFAULT MODE

        public void OnGet()
        {

            isPreviewPage = false;
        }

        // PREVIEW MODE (AFTER SUBMITTING)

        public async Task<IActionResult> OnPost()
        {
            

            if (await isValid())
            {
                if (ModelState.IsValid)
                {
                    isPreviewPage = true;
                    try
                    {
                        // ADD TO DATABASE
                        //_context.__MODEL__.Add(__MODEL__);
                        //_context.SaveChanges();

                        return RedirectToPage("Preview");
                    }
                    catch { }
                }
            }
            else
            {
                ModelState.AddModelError("bridgeGreetings.reCaptcha", "Please select the recaptcha");
            }

            return Page();
        }

        //BRIDGE TO GREETINGS MODEL
        [BindProperty]
        public Greetings bridgeGreetings { get; set; }

        //TEST IF USER IS LOOKING AT PREVIEW OR FORM
        public bool isPreviewPage { get; set; }

        // RE-CAPTCHA VALIDATION
        private async Task<bool> isValid()
        {
            var response = this.HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(response))
                return false;

            try
            {
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>();
                    values.Add("secret", "6Le5_S0UAAAAADVyjgJOG_4ptTimv71jLTGh8ZI0");
                    values.Add("response", response);
                    //values.Add("remoteip", this.HttpContext.Connection.RemoteIpAddress.ToString());

                    var query = new FormUrlEncodedContent(values);


                    var post = client.PostAsync("https://www.google.com/recaptcha/api/siteverify", query);

                    var json = await post.Result.Content.ReadAsStringAsync();

                    if (json == null)
                        return false;

                    var results = JsonConvert.DeserializeObject<dynamic>(json);

                    return results.success;
                }

            }
            catch { }


            return false;
        }






    }

}
