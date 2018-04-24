using DeliverySystem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ModelUnitTests
{
    public class CategoryUnitTests
    {
        private string Name { get; set; } = "Sample";
        public int Id { get; set; } = 1;
        public Category Category { get; set; }

        [Fact]
        [Trait("Category", "throws exception")]
        public void After_Passing_Nullable_Name()
        {
            Category = new Category()
            {
                Id = Id,
                Name = null
            };
            var context = new ValidationContext(instance: Category,
                                                serviceProvider: null,
                                                items: null);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(instance: Category,
                                                    validationContext: context,
                                                    validationResults: result,
                                                    validateAllProperties: true);
            Assert.False(isValid, "Name is null or empty");
        }

        [Fact]
        [Trait("Category", "returns correct")]
        public void CategoryName()
        {
            Category = new Category()
            {
                Id = Id,
                Name = Name
            };
            Assert.Equal(Name, Category.Name);
        }

        [Theory]
        [Trait("Category", "throws exception")]
        [InlineData("abc")]
        public void After_Passing_IncapitalizedName(string name)
        {
            Category = new Category()
            {
                Id = Id,
                Name = name
            };
            var context = new ValidationContext(instance: Category,
                                                serviceProvider: null,
                                                items: null);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(instance: Category,
                                                    validationContext: context,
                                                    validationResults: result,
                                                    validateAllProperties: true);
            Assert.False(isValid, "Word starts with upper-case and contains only letters.");
        }

        [Theory]
        [Trait("Category", "throws exception")]
        [InlineData("A")]
        public void When_Name_Is_Shorter_Than_Required_Length(string name)
        {
            Category = new Category()
            {
                Id = Id,
                Name = name
            };
            var context = new ValidationContext(instance: Category,
                                                serviceProvider: null,
                                                items: null);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(instance: Category,
                                                    validationContext: context,
                                                    validationResults: result,
                                                    validateAllProperties: true);
            Assert.False(isValid, "Please use name of length in range 3..30");
        }
    }
}
