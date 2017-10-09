using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace halloween.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Hey Dummy, that was rude!")]
        public string mesgfromuser { get; set; }

        [BindProperty]
        public string recipient { get; set; }

        [BindProperty]
        public string subject { get; set; }

        [BindProperty]
        public string sendersname { get; set; }

        [BindProperty]
        public string sendersemail { get; set; }
       

        public void OnGet()
        {

        }
        public void OnPost()
        {

        }


    }     
   
}
