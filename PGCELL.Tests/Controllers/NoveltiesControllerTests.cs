using Microsoft.AspNetCore.Mvc;
using Moq;
using PGCELL.Backend.Controllers;
using PGCELL.Backend.UnitsOfWork;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Shared.Responses;

namespace PGCELL.Tests.Controllers
{
    [TestClass]
    public class NoveltiesControllerTests
    {
        private Mock<IGenericUnitOfWork<Novelty>> _mockGenericUnitOfWork = null!;
        private Mock<INoveltiesUnitOfWork> _mockNoveltyUnitOfWork = null!;
        private NoveltiesController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockGenericUnitOfWork = new Mock<IGenericUnitOfWork<Novelty>>();
            _mockNoveltyUnitOfWork = new Mock<INoveltiesUnitOfWork>();
            _controller = new NoveltiesController(_mockGenericUnitOfWork.Object, _mockNoveltyUnitOfWork.Object);
        }

        [TestMethod]
        public async Task GetComboAsync_ReturnsOkObjectResult()
        {
            // Arrange
            var comboData = new List<Novelty>
            {
                new Novelty {  Id = 1, Name = "Vacaciones" }
            };
            _mockNoveltyUnitOfWork.Setup(x => x.GetComboAsync()).ReturnsAsync(comboData);

            // Act
            var result = await _controller.GetComboAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(comboData, okResult!.Value);
            _mockNoveltyUnitOfWork.Verify(x => x.GetComboAsync(), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new Response<IEnumerable<Novelty>> { WasSuccess = true };
            _mockNoveltyUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(response.Result, okResult!.Value);
            _mockNoveltyUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetAsync_ReturnsBadRequestResult_WhenWasSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var response = new Response<IEnumerable<Novelty>> { WasSuccess = false };
            _mockNoveltyUnitOfWork.Setup(x => x.GetAsync(pagination)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockNoveltyUnitOfWork.Verify(x => x.GetAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsOkObjectResult_WhenWasSuccessIsTrue()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var action = new Response<int> { WasSuccess = true, Result = 5 };
            _mockNoveltyUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(action);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.AreEqual(action.Result, okResult!.Value);
            _mockNoveltyUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }

        [TestMethod]
        public async Task GetPagesAsync_ReturnsBadRequestResult_WhenWasSuccessIsFalse()
        {
            // Arrange
            var pagination = new PaginationDTO();
            var action = new Response<int> { WasSuccess = false };
            _mockNoveltyUnitOfWork.Setup(x => x.GetTotalPagesAsync(pagination)).ReturnsAsync(action);

            // Act
            var result = await _controller.GetPagesAsync(pagination);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            _mockNoveltyUnitOfWork.Verify(x => x.GetTotalPagesAsync(pagination), Times.Once());
        }
    }
}