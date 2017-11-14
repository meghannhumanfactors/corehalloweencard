using halloween.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace halloween.Pages
{
    public class UpdateModel : PageModel
    {
        // DEFAULT MODE
        public IActionResult OnGet(int id = 0)
        {
            if (id > 0)
            {
                bridgeGreetings = _myDB.Greetings.Find(id);
                return Page();
            }
            else
            {
                return RedirectToPage("index");
            }
        }

        // EMAIL-RELATED
        public string Message { get; set; }
        public IActionResult OnPost()
        {


            try
            {
                // DB-RELATED: UPDATE RECORD ON THE DATABASE 
                _myDB.Greetings.Update(bridgeGreetings);
                _myDB.SaveChanges();

                return RedirectToPage("preview", new { ID = bridgeGreetings.ID });
            }
            catch
            {
                Message = "Yikes, your greeting can't be sent. Please check your spelling and try again.";
            }


            return Page();
        }


        // BRIDGE TO GREETINGS MODEL
        [BindProperty]
        public Greetings bridgeGreetings { get; set; }

        // HEY, CONNECT MY DATABASE TO THIS MODEL
        private DB _myDB;
        public UpdateModel(DB myDB)
        {
            _myDB = myDB;
        }


    }
}