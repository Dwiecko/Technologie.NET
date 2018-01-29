using DeliverySystem.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace DeliverySystem.Models
{
    public class Delivery : IValidatableObject
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
        public int DeliveryID { get; set; }

        [DisplayName("Product")]
        public int ProductID { get; set; }

        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryStart { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryEnd { get; set; }

        [Required]
        [CheckPhoneNumer]
        public string PhoneNumber { get; set; }

        [Required]
        [Capitalize]
        public string City { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Range(minimum: 1, maximum: 10000.00)]
        public decimal EstimatedWeight { get; set; }

        [Range(minimum: 1, maximum: 100000.00)]
        public decimal Amount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.DeliveryEnd < this.DeliveryStart)
            {
                yield return
                  new ValidationResult(errorMessage: "DeliveryEnd must be greater or equal to DeliveryStart",
                                       memberNames: new[] { "DeliveryEnd" });
            }
        }
    }
}