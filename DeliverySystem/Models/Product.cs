using DeliverySystem.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliverySystem.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [Capitalize]
        [StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Please use name of length in range 3..30")]
        public string Name { get; set; }

        public ICollection<Delivery> Delivery { get; set; }
    }
}