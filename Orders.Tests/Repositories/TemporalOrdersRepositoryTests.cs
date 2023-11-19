﻿using Microsoft.EntityFrameworkCore;
using Moq;
using PGCELL.Backend.Data;
using PGCELL.Backend.Helpers;
using PGCELL.Backend.Repositories;
using PGCELL.Shared.DTOs;
using PGCELL.Shared.Entites;
using PGCELL.Tests.Shared;

namespace PGCELL.Tests.Repositories
{
    [TestClass]
    public class TemporalOrdersRepositoryTests
    {
        private TemporalOrdersRepository _repository = null!;
        private DataContext _context = null!;
        private Mock<IUserHelper> _userHelperMock = null!;
        private DbContextOptions<DataContext> _options = null!;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(_options);
            _userHelperMock = new Mock<IUserHelper>();
            _repository = new TemporalOrdersRepository(_context, _userHelperMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task AddFullAsync_ValidData_AddsTemporalOrder()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User { Email = email, Address = "Any", Document = "Any", FirstName = "John", LastName = "Doe" };
            _context.Users.Add(user);
            _context.SaveChanges();

            var product = new Product { Id = 1, Name = "Some", Description = "Some" };
            _context.Products.Add(product);
            _context.SaveChanges();

            var dto = new TemporalOrderDTO
            {
                ProductId = product.Id,
                Quantity = 1
            };

            _userHelperMock.Setup(x => x.GetUserAsync(email))
                .ReturnsAsync(user);

            // Act
            var result = await _repository.AddFullAsync(email, dto);

            // Assert
            Assert.IsTrue(result.WasSuccess);
            Assert.AreEqual(1, _context.TemporalPGCELL.Count());
            var temporalOrder = _context.TemporalPGCELL.First();
            Assert.AreEqual(product.Id, temporalOrder.ProductId);
            Assert.AreEqual(1, temporalOrder.Quantity);
        }

        [TestMethod]
        public async Task AddFullAsync_WithException_ReturnsError()
        {
            // Arrange
            var exceptionalContext = new ExceptionalDataContext(_options);
            var email = "test@example.com";
            var user = new User { Email = email, Address = "Any", Document = "Any", FirstName = "John", LastName = "Doe" };
            exceptionalContext.Users.Add(user);
            exceptionalContext.SaveChanges();

            var product = new Product { Id = 1, Name = "Some", Description = "Some" };
            exceptionalContext.Products.Add(product);
            exceptionalContext.SaveChanges();

            var dto = new TemporalOrderDTO
            {
                ProductId = product.Id,
                Quantity = 1
            };

            _userHelperMock.Setup(x => x.GetUserAsync(email))
                .ReturnsAsync(user);

            var repository = new TemporalOrdersRepository(exceptionalContext, _userHelperMock.Object);

            // Act
            var result = await repository.AddFullAsync(email, dto);

            // Assert
            Assert.IsFalse(result.WasSuccess);
        }

        [TestMethod]
        public async Task AddFullAsync_ValidUser_ReturnsError()
        {
            // Arrange
            var email = "test@example.com";
            var product = new Product { Id = 1, Name = "Some", Description = "Some" };
            _context.Products.Add(product);
            _context.SaveChanges();

            var dto = new TemporalOrderDTO
            {
                ProductId = product.Id,
                Quantity = 1
            };

            // Act
            var result = await _repository.AddFullAsync(email, dto);

            // Assert
            Assert.IsFalse(result.WasSuccess);
            Assert.AreEqual("Usuario no existe", result.Message);
        }

        [TestMethod]
        public async Task AddFullAsync_InvalidProduct_ReturnsError()
        {
            // Arrange
            var email = "test@example.com";
            var dto = new TemporalOrderDTO
            {
                ProductId = 999,
                Quantity = 1
            };

            // Act
            var result = await _repository.AddFullAsync(email, dto);

            // Assert
            Assert.IsFalse(result.WasSuccess);
            Assert.AreEqual("Producto no existe", result.Message);
        }

        [TestMethod]
        public async Task GetAsync_UserExists_ReturnsTemporalOrders()
        {
            // Arrange
            var email = "test@example.com";
            var product = new Product { Id = 1, Name = "Some", Description = "Some" };
            _context.Products.Add(product);
            var user = new User { Email = email, Address = "Any", Document = "Any", FirstName = "John", LastName = "Doe" };
            _context.Users.Add(user);
            _context.SaveChanges();

            var temporalOrders = new List<TemporalOrder>
            {
                new TemporalOrder { User = user, Product = product, Quantity = 1 },
                new TemporalOrder { User = user, Product = product, Quantity = 2 }
            };

            _context.TemporalPGCELL.AddRange(temporalOrders);
            _context.SaveChanges();

            // Act
            var result = await _repository.GetAsync(email);

            // Assert
            Assert.IsTrue(result.WasSuccess);
            Assert.AreEqual(2, result.Result!.Count());
        }

        [TestMethod]
        public async Task GetCountAsync_UserWithNoOrders_ReturnsZero()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            var result = await _repository.GetCountAsync(email);

            // Assert
            Assert.IsTrue(result.WasSuccess);
            Assert.AreEqual(0, result.Result);
        }

        [TestMethod]
        public async Task GetCountAsync_UserDoesNotExist_ReturnsZero()
        {
            // Arrange
            var email = "nonexistent@example.com";

            // Act
            var result = await _repository.GetCountAsync(email);

            // Assert
            Assert.IsTrue(result.WasSuccess);
            Assert.AreEqual(0, result.Result);
        }

        [TestMethod]
        public async Task PutFullAsync_OrderExists_UpdatesOrder()
        {
            // Arrange
            var temporalOrder = new TemporalOrder { Id = 1, Remarks = "Old Remarks", Quantity = 5 };
            _context.TemporalPGCELL.Add(temporalOrder);
            await _context.SaveChangesAsync();

            var updateDTO = new TemporalOrderDTO { Id = 1, Remarks = "New Remarks", Quantity = 10 };

            // Act
            var result = await _repository.PutFullAsync(updateDTO);

            // Assert
            Assert.IsTrue(result.WasSuccess);
            Assert.AreEqual(updateDTO.Remarks, result.Result!.Remarks);
            Assert.AreEqual(updateDTO.Quantity, result.Result.Quantity);
        }

        [TestMethod]
        public async Task PutFullAsync_OrderDoesNotExist_ReturnsErrorResponse()
        {
            // Arrange
            var updateDTO = new TemporalOrderDTO { Id = 99, Remarks = "New Remarks", Quantity = 10 };

            // Act
            var result = await _repository.PutFullAsync(updateDTO);

            // Assert
            Assert.IsFalse(result.WasSuccess);
            Assert.AreEqual("Registro no encontrado", result.Message);
        }

        [TestMethod]
        public async Task GetAsync_OrderExists_ReturnsOrder()
        {
            // Arrange
            var email = "test@example.com";
            var user = new User { Email = email, Address = "Any", Document = "Any", FirstName = "John", LastName = "Doe" };
            _context.Users.Add(user);
            _context.SaveChanges();

            var product = new Product { Id = 1, Name = "Some", Description = "Some" };
            _context.Products.Add(product);
            _context.SaveChanges();

            var temporalOrder = new TemporalOrder { Id = 1, User = user, Product = product };
            _context.TemporalPGCELL.Add(temporalOrder);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAsync(1);

            // Assert
            Assert.IsTrue(result.WasSuccess);
            Assert.IsNotNull(result.Result);
            Assert.AreEqual(1, result.Result.Id);
        }

        [TestMethod]
        public async Task GetAsync_OrderDoesNotExist_ReturnsErrorResponse()
        {
            // Act
            var result = await _repository.GetAsync(99);

            // Assert
            Assert.IsFalse(result.WasSuccess);
            Assert.AreEqual("Registro no encontrado", result.Message);
        }
    }
}