using DeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ModelUnitTests
{
    public class DeliveryUnitTests
    {
        [Fact]
        [Trait("Delivery", "returns true")]
        private void When_Is_Added_Properly()
        {
            var context = new ValidationContext(_delivery, null, null);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_delivery, context, result, true);
            Assert.True(isValid);
        }

        [Fact]
        [Trait("Delivery", "returns correct")]
        private void StreetName_After_Using_Getter()
        {
            _delivery.StreetName = StreetName;

            Assert.Equal("Puchowa 20", _delivery.StreetName);
        }

        [Fact]
        [Trait("Delivery", "throws exception")]
        private void After_Passing_End_Date_Before_Start_Date()
        {
            _delivery.DeliveryStart = new DateTime(2000, 12, 13);
            _delivery.DeliveryEnd = new DateTime(2000, 12, 12);

            var context = new ValidationContext(_delivery, null, null);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_delivery, context, result, true);

            Assert.False(isValid, "Start date must be before end date");
        }

        [Fact]
        [Trait("Delivery", "throws exception")]
        private void After_Passing_Not_Capitalized_City_Name()
        {
            _delivery.City = "gdynia";

            var context = new ValidationContext(_delivery, null, null);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_delivery, context, result, true);

            Assert.False(isValid, "City starts with uppercase and contains only letters");
        }

        [Fact]
        [Trait("Delivery", "throws exception")]
        private void After_Passing_Empty_City_Name()
        {
            _delivery.City = "";

            var context = new ValidationContext(_delivery, null, null);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_delivery, context, result, true);

            Assert.False(isValid, "City can not be null or empty.");
        }

        [Fact]
        [Trait("Delivery", "throws exception")]
        private void After_Passing_Phone_Number_In_Incorrect_Format()
        {
            _delivery.PhoneNumber = "123- 123-123";

            var context = new ValidationContext(_delivery, null, null);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_delivery, context, result, true);

            Assert.False(isValid, "Phone number correct format: XXX-XXX-XXX where X contains only numbers");
        }

        [Fact]
        [Trait("Delivery", "throws exception")]
        private void After_Passing_Empty_Phone_Number()
        {
            _delivery.PhoneNumber = "";

            var context = new ValidationContext(_delivery, null, null);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_delivery, context, result, true);

            Assert.False(isValid, "Phone number can not be null or empty.");
        }

        [Fact]
        [Trait("Delivery", "throws exception")]
        private void After_Passing_Amount_Bigger_Than_Maximum()
        {
            _delivery.Amount = 999999999.00M;

            var context = new ValidationContext(_delivery, null, null);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_delivery, context, result, true);

            Assert.False(isValid, "Amount must be between 1 and 100000.00.");
        }
        private Delivery _delivery;
        public int Id { get; set; } = 100;
        public int ProductId { get; set; } = 2;
        public int CategoryId { get; set; } = 2;
        public DateTime DeliveryStart { get; set; } = new DateTime(2000, 12, 13);
        public DateTime DeliveryEnd { get; set; } = new DateTime(2000, 12, 14);
        public string PhoneNumber { get; set; } = "123-456-789";
        public string City { get; set; } = "Gdynia";
        public string StreetName { get; set; } = "Puchowa 20";
        public decimal EstimatedWeight { get; set; } = 20.00M;
        public decimal Amount { get; set; } = 2.00M;
        public DeliveryUnitTests()
        {
            InitializeSampleDelivery();
        }
        private void InitializeSampleDelivery()
        {
            _delivery = new Delivery()
            {
                Id = Id,
                ProductID = ProductId,
                CategoryID = CategoryId,
                DeliveryStart = DeliveryStart,
                DeliveryEnd = DeliveryEnd,
                PhoneNumber = PhoneNumber,
                City = City,
                StreetName = StreetName,
                EstimatedWeight = EstimatedWeight,
                Amount = Amount
            };
        }
    }
}
