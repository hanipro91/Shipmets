using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShipmentTracking.Models
{
    public class Shipments
    {
        [NotMapped]
        private const double COST = 0.5; //$ for 1kg and 1day


        //[Key] or name 'ID' or combined class name with id e.g 'ShipmentId'
        public int ID { get; set; }
        [MaxLength(50), Required]
        public string Source { get; set; }
        [MaxLength(50), Required]
        public string location { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Estimated Time")]
        public int ExepectedEstmatedTime { get; set; }//expected days to reach
        
        [Display(Name = "Duration")]
        public int RealEstmatedTime { get; set; }//real days reached
        [Required]
        public float Weight { get; set; }

        [Column(TypeName ="money")]
        public decimal Price { get; set; }

        [Required]
        public int CustomersID { get; set; }

        [ForeignKey("CustomersID")]
        public Customers Customer;

        
        [NotMapped]
        public int DeliverPercent
            => !IsDelivered && DateTime.Now.Subtract(Date).Days < ExepectedEstmatedTime ?
                    DateTime.Now.Subtract(Date).Days / ExepectedEstmatedTime * 100 : 100;
                
        [NotMapped]
        public DateTime ExpectedDeleiverDate
            => Date.AddDays(ExepectedEstmatedTime);

        [NotMapped]
        public DateTime DeleiveredDate
            => Date.AddDays(RealEstmatedTime);

        [NotMapped]
        public bool IsDelivered => RealEstmatedTime > 0;

        public void CalculatePrice()
        {
            Price = Convert.ToDecimal(Weight * ExepectedEstmatedTime * COST);
        }
        
    }
}
