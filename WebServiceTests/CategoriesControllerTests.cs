using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataServiceLib;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebService.Controllers;
using WebService.Models;
using Xunit;

namespace WebServiceTests
{
    public class CategoriesControllerTests
    {
        private Mock<IDataService> _dataServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUrlHelper> _urlMock;

        public CategoriesControllerTests()
        {
            _dataServiceMock = new Mock<IDataService>();
            _mapperMock = new Mock<IMapper>();
            _urlMock = new Mock<IUrlHelper>();
        }

        [Fact]
        public void CreateCategoryShouldCallDataService()
        {
            var ctrl = new CategoriesController(_dataServiceMock.Object, _mapperMock.Object);

            ctrl.CreateCategory(new CategoryForCreationOrUpdateDto());

            _dataServiceMock.Verify(x => x.CreateCategory(It.IsAny<Category>()), Times.Once);
        }
    }
}
