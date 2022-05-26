using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShipmentTracking.Models
{
    public class Users
    {
        public int ID { get; set; }

        [MaxLength(50), Required]
        [Remote("ValidateUniqUser", "Users", 
            ErrorMessage = "Invalid User. Try another one.", 
            HttpMethod = "POST")]
        public string UserName { get; set; }

        [MaxLength(50), Required]
        public string Password { get; set; }

        [MaxLength(50), Required]
        public string Name { get; set; }

        [MaxLength(10), Required]
        public string Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [MaxLength(50), Required, EmailAddress]
        [Remote("ValidateUniqEmail", "Users",
            ErrorMessage = "Email already exist. Try another one.", 
            HttpMethod = "POST")]
        public string Email { set; get; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        public string Picture { get; set; }

    }
}
