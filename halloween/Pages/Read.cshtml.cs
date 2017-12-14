using halloween.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace halloween.Pages
{
    public class ReadModel : PageModel
    {
        // DEFAULT MODE
        public IActionResult OnGet(int id = 0)
        {
            if (id > 0)
            {
                bridgeGreetings = _myDB.Greetings.Find(id);
                
            }
           if (bridgeGreetings== null){
                return RedirectToPage("index");
            }

            return Page();
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

                return RedirectToPage("read", new { ID = bridgeGreetings.ID });
            }
            catch
            {
                Message = "Yikes, your greeting can't be loaded. Please try again.";
            }


            return Page();
        }


        // BRIDGE TO GREETINGS MODEL
        [BindProperty]
        public Greetings bridgeGreetings { get; set; }

        // HEY, CONNECT MY DATABASE TO THIS MODEL
        private DB _myDB;
        public ReadModel(DB myDB)
        {
            _myDB = myDB;
        }


    }
}