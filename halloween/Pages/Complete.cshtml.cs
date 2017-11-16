using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using halloween.Model;

namespace halloween.Pages
{
    public class CompleteModel : PageModel
    {
        // BRIDGE TO GREETINGS MODEL
        [BindProperty]
        public Greetings bridgeGreetings { get; set; }

        // HEY, CONNECT MY DATABASE TO THIS MODEL
        private DB _myDB;
        public CompleteModel(DB myDB)
        {
            _myDB = myDB;
        }
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
    }
}