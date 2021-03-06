﻿using DeliverySystem.Attributes;
using DeliverySystem.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliverySystem.Models
{
    public class Delivery : IValidatableObject, IEntity
    {
        public int Id { get; set; }

        [DisplayName("Product")]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        [DisplayName("Category")]
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryStart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryEnd { get; set; }


        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [CheckPhoneNumer]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Capitalize]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
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