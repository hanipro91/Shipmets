using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShipmentTracking.Models
{
    public class Customers
    {
      
        public int ID { get; set; }

        [MaxLength(50), Required]
        public string Name { get; set; }

        [MaxLength(50), Required]
        public string Email { get; set; }

        [MaxLength(50), Required]
        public string Password { get; set; }

        [Required, MaxLength(30)]
        public string Phone { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required, MaxLength(10)]
        public string Sex { get; set; }

        [Required, MaxLength(50)]
        public string Country { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(50)]
        public string Address { get; set; }

        public string Picture { get; set; }

        public virtual ICollection<Shipments> Shipments { get; set; }

    }
}
