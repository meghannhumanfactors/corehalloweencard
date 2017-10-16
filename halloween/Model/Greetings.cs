﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace halloween.Model
{
    public class Greetings
    { 
        [DisplayName("To")]
        [Required (ErrorMessage = "Required!")]
        public string recipient { get; set; }

        [DisplayName("From: Your Name")]
        [Required(ErrorMessage = "Required!")]
        public string sendersnamet { get; set; }

        [DisplayName("From: Your Email")]
        [Required(ErrorMessage = "Required!")]
        public string sendersemail { get; set; }

        [DisplayName("Subject")]
        [Required(ErrorMessage = "Required!")]
        public string subject { get; set; }

        [DisplayName("Enter your message")]
        [Required(ErrorMessage = "Required!")]
        public string mesgfromuser { get; set; }



        [Required(ErrorMessage = "Required!")]
        public string agree { get; set; }


        [Required(ErrorMessage = "Required!")]
        public string createDate { get; set; }


        [Required(ErrorMessage = "Required!")]
        public string createIP { get; set; }


       
        public string sendDate { get; set; }
        public string sendIP { get; set; }
    }
}
