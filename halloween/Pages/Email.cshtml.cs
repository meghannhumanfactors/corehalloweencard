using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using halloween.Model;

namespace halloween.Pages
{
    public class EmailModel : PageModel
    {
        [BindProperty]
        public Greetings Greetings { get; set; }
        private Database _dbContext { get; set; }
        private IConfiguration _configuration { get; set; }

        //hey, Create the database connection through the constructor
        public EmailModel(Database dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public void OnGet(int ID = 0)
        {
            if (ID > 0)
            {
                Greetings = _dbContext.Greetings.Find(ID);
            }
        }

    }
}