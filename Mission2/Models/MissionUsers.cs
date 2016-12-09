using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mission2.Models
{
    //This table represents the MissionUsers Table
    [Table("MissionUsers")]
    public class MissionUsers
    {
        [Key]
        public int UserID { get; set; }

        //Email: must be in proper email format!
        [Required(ErrorMessage="Please enter your email!")]
        [DisplayName("Email: ")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please enter a valid email address!")]
        public string UserEmail { get; set; }

        //Password: displays as DOTS because it's a password. Secure.
        [Required(ErrorMessage="Please enter a password!")]
        [DisplayName("Password: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //First Name: required
        [Required(ErrorMessage="Please enter your first name!")]
        [DisplayName("First Name: ")]
        public string FirstName { get; set; }

        //Last Name: required
        [Required(ErrorMessage="Please enter your last name!")]
        [DisplayName("Last Name: ")]
        public string LastName { get; set; }

    }
}