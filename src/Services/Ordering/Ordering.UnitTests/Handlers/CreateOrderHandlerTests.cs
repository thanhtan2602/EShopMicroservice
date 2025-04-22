using Microsoft.EntityFrameworkCore;
using Moq;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Models;

namespace Ordering.UnitTests.Handlers
{
    public class CreateOrderHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _mockDbContext;
        private readonly CreateOrderHandler _handler;

        public CreateOrderHandlerTests()
        {
            _mockDbContext = new Mock<IApplicationDbContext>();

            // Setup mock for Orders DbSet
            var mockOrders = new Mock<DbSet<Order>>();
            _mockDbContext.Setup(x => x.Orders).Returns(mockOrders.Object);

            _handler = new CreateOrderHandler(_mockDbContext.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateOrderSuccessfully()
        {
            // Arrange
            var orderDto = new OrderDto(
                Id: Guid.NewGuid(),
                CustomerId: Guid.NewGuid(),
                OrderName: "Test Order",
                ShippingAddress: new AddressDto
                (
                    FirstName: "John",
                    LastName: "Doe",
                    EmailAddress: "john@example.com",
                    AddressLine: "123 Street",
                    Country: "US",
                    State: "NY",
                    ZipCode: "10001"
                ),
                BillingAddress: new AddressDto(FirstName: "John", LastName: "Doe", EmailAddress: "john@example.com", AddressLine: "123 Street", Country: "US", State: "NY", ZipCode: "10001"),
                Payment: new PaymentDto(CardName: "John Doe", CardNumber: "1234567890123456", Expiration: "12/25", Cvv: "123", PaymentMethod: 1),
                Status: Domain.Enums.OrderStatus.Pending,
                OrderItems: new List<OrderItemDto>
                {
                    new OrderItemDto(OrderId: Guid.NewGuid(), ProductId: Guid.NewGuid(), Quantity: 2, Price: 10)
                }
            );

            var command = new CreateOrderCommand(orderDto);

            _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockDbContext.Verify(x => x.Orders.Add(It.IsAny<Order>()), Times.Once);
            _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<CreateOrderResult>(result);
            Assert.NotEqual(Guid.Empty, result.Id);
        }
    }

}
