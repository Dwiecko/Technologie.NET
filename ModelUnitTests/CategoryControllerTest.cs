using DeliverySystem.Controllers;
using DeliverySystem.Models;
using DeliverySystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ModelUnitTests
{
    public class CategoryControllerTest
    {
        [Fact]
        [Trait("Categories Controller", "returns correct")]
        public void NumberOfModels_From_Repository()
        {
            var items = new List<Category>
            {
                new Category(),
                new Category()
            };
            var repositoryMock = new Mock<IRepository<Category>>();
            repositoryMock.Setup(x => x.GetAll()).Returns(items);
            var controller = new CategoriesController(repositoryMock.Object);

            var result = controller.Index();

            var viewResult = (ViewResult)result;
            var model = (IList<Category>)viewResult.Model;

            Assert.Equal(2, model.Count);
        }

        [Fact]
        [Trait("Categories Controller", "returns empty")]
        public void Model_When_ThereAreNoResults_From_Repository()
        {
            var repositoryMock = new Mock<IRepository<Category>>();
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Category>());
            var controller = new CategoriesController(repositoryMock.Object);

            var result = controller.Index();

            var viewResult = (ViewResult)result;
            var model = (IList<Category>)viewResult.Model;

            Assert.Empty(model);
        }
       
        [Fact]
        [Trait("Categories Controller", "returns correct")]
        public void Model_After_GivingExistingCategoryId()
        {
            // Arrange
            var item1 = new Category { Name = "Item1" };
            var item2 = new Category { Name = "Item2" };
            var item3 = new Category { Name = "Item3" };
            var expectedName = item2.Name;

            var categoryMock = new Mock<IRepository<Category>>();
            categoryMock.Setup(x => x.Get(1)).Returns(item1);
            categoryMock.Setup(x => x.Get(2)).Returns(item2);
            categoryMock.Setup(x => x.Get(3)).Returns(item3);

            var controller = new CategoriesController(categoryMock.Object);

            // Act
            var result = controller.Details(2);

            // Assert
            var viewResult = (ViewResult)result;
            var model = (Category)viewResult.Model;

            Assert.Equal(expectedName, model.Name);
        }
    }
}
