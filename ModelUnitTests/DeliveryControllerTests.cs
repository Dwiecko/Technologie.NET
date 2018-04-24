using DeliverySystem.Controllers;
using DeliverySystem.Models;
using DeliverySystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ModelUnitTests
{
    public class DeliveryControllerTests
    {
        [Fact]
        [Trait("Delivery Controller", "returns correct")]
        public void NumberOfModels_From_Repository()
        {
            var deliveries = new List<Delivery>
            {
                new Delivery(),
                new Delivery()
            };
            var categories = new List<Category>
            {
                new Category(),
                new Category()
            };
            var products = new List<Product>
            {
                new Product(),
                new Product()
            };

            var deliveriesRepositoryMock = new Mock<IDeliveriesRepository>();
            var categoriesRepositoryMock = new Mock<IRepository<Category>>();
            var productsRepositoryMock = new Mock<IRepository<Product>>();

            deliveriesRepositoryMock.Setup(x => x.GetAll()).Returns(deliveries);
            categoriesRepositoryMock.Setup(x => x.GetAll()).Returns(categories);
            productsRepositoryMock.Setup(x => x.GetAll()).Returns(products);

            var controller = new DeliveriesController(deliveriesRepositoryMock.Object, 
                                                      categoriesRepositoryMock.Object, 
                                                      productsRepositoryMock.Object);

            var result = controller.Index();

            var viewResult = (ViewResult)result;
            var model = (IList<Delivery>)viewResult.Model;

            Assert.Equal(2, model.Count);
        }

    }
}
