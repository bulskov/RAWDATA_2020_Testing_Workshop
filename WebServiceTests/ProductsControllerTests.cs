using System;
using AutoMapper;
using DataServiceLib;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebService.Controllers;
using WebService.Models;
using Xunit;

namespace WebServiceTests
{
    public class ProductsControllerTests
    {
        private Mock<IDataService> _dataServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUrlHelper> _urlMock;

        public ProductsControllerTests()
        {
            _dataServiceMock = new Mock<IDataService>();
            _mapperMock = new Mock<IMapper>();
            _urlMock = new Mock<IUrlHelper>();
        }

        [Fact]
        public void GetProductWithValidIdSouldRetrunOk()
        {
            _dataServiceMock.Setup(x => x.GetProduct(1)).Returns(new Product {Category = new Category()});
            _mapperMock.Setup(x => x.Map<ProductDetailsDto>(It.IsAny<Product>())).Returns(new ProductDetailsDto());

            var ctrl = new ProductsController(_dataServiceMock.Object, _mapperMock.Object);
            ctrl.Url = _urlMock.Object;

            var response = ctrl.GetProduct(1);

            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetProductWithInvalidIdSouldRetrunNotFound()
        {
            var ctrl = new ProductsController(_dataServiceMock.Object, null);

            var response = ctrl.GetProduct(1023);

            response.Should().BeOfType<NotFoundResult>();
        }
    }
}
