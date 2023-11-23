﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using PGCELL.Backend.Controllers;
using PGCELL.Backend.UnitsOfWork;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Tests.Controllers
{
    [TestClass]
    public class CountriesControllerTests
    {
        private Mock<IGenericUnitOfWork<Country>> _mockGenericUnitOfWork = null!;
        private Mock<ICountriesUnitOfWork> _mockCountriesUnitOfWork = null!;
        private CountriesController _controller = null!;

        [TestInitialize]
        public void Initialize()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Country>>();
            _mockCountriesUnitOfWork = new Mock<ICountriesUnitOfWork>();
            _controller = new CountriesController(_mockGenericUnitOfWork.Object, _mockCountriesUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetComboAsync_ShouldReturnOk()
        {
            // Arrange
            var response = new List<Country> { new Country { Id = 1, Name = "Colombia" } };
            _mockCountriesUnitOfWork.Setup(x => x.GetComboAsync())
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetComboAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response, okResult?.Value);
            _mockCountriesUnitOfWork.Verify(x => x.GetComboAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnOk()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var countries = new List<Country>
            {
                new Country { Id = 1, Name = "Colombia" },
                new Country { Id = 2, Name = "Argentina" }
            };
            var response = new Response<IEnumerable<Country>> { WasSuccess = true, Result = countries };
            _mockCountriesUnitOfWork.Setup(x => x.GetAsync(pagination))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(countries, okResult?.Value);
            _mockCountriesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_Pagination_ShouldReturnBadRequest()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var countries = new List<Country>
            {
                new Country { Id = 1, Name = "Country1" },
                new Country { Id = 2, Name = "Country2" }
            };
            var response = new Response<IEnumerable<Country>> { WasSuccess = false, Result = countries };
            _mockCountriesUnitOfWork.Setup(x => x.GetAsync(pagination))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCountriesUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ShouldReturnOk()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var totalPages = 5;
            var response = new Response<int> { WasSuccess = true, Result = totalPages };
            _mockCountriesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(totalPages, okResult?.Value);
            _mockCountriesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var totalPages = 5;
            var response = new Response<int> { WasSuccess = false };
            _mockCountriesUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockCountriesUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ById_ShouldReturnOk()
        {
            // Arrange
            var countryId = 1;
            var country = new Country { Id = countryId, Name = "Country1" };
            var response = new Response<Country> { WasSuccess = true, Result = country };
            _mockCountriesUnitOfWork.Setup(x => x.GetAsync(countryId)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(countryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(country, okResult?.Value);
            _mockCountriesUnitOfWork.Verify(x => x.GetAsync(countryId), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ById_ShouldReturnNotFound()
        {
            // Arrange
            var countryId = 1;
            var response = new Response<Country> { WasSuccess = false, Message = "Not Found" };
            _mockCountriesUnitOfWork.Setup(x => x.GetAsync(countryId)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(countryId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("Not Found", notFoundResult?.Value);
            _mockCountriesUnitOfWork.Verify(x => x.GetAsync(countryId), Times.Once());
        }
    }
}