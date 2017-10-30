using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace halloween.Model
{
    public class Greetings
    {

        //HEY, ADD A UNIQUE IDENTIFIER
        [Key]
        public int ID { get; set; }

        [DisplayName("To: Name")]
        [Display(Prompt = "For example, Freddy Krueger ")]
        [Required(ErrorMessage = "Required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "You must enter between 3 to 100 characters")]
        public string recipientname { get; set; }


        [DisplayName("To: Email")]
        [Display(Prompt = "For example, freddykrueger@domain.com")]
        [Required (ErrorMessage = "Required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "You must enter a valid email")]
        public string recipientemail { get; set; }

        [DisplayName("From: Your Name")]
        [Display(Prompt = "e.g., Michael Myers")]
        [Required(ErrorMessage = "Required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "You must enter between 3 to 100 characters")]
        public string sendersname { get; set; }

        [DisplayName("From: Your Email")]
        [Display(Prompt = "For example, MichaelMyers@domain.com")]
        [Required(ErrorMessage = "Required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "You must enter a valid email")]
        public string sendersemail { get; set; }

        [DisplayName("Subject")]
        [Display(Prompt = "e.g., Halloween Egreeting")]
        [Required(ErrorMessage = "Required!")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "You must enter between 2 to 100 characters")]
        public string subject { get; set; }

        [DisplayName("Enter your message")]
        [Display(Prompt = "e.g., We Witch you a Happy Hallween. Bugs & Hisses, Michael")]
        [Required(ErrorMessage = "Required!")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "You must enter between 2 to 500 characters")]
        public string mesgfromuser { get; set; }



        
        public bool terms { get; set; }


       
        public string createDate { get; set; }


        
        public string createIP { get; set; }


       
        public string sendDate { get; set; }
        public string sendIP { get; set; }

        public string reCaptcha { get; set; }
    }
}
