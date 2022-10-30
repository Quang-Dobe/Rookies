using AutoMapper;
using ECommerce.BackendAPI.Controllers;
using ECommerce.BackendAPI.Profiles;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using ECommerce.TestBackendAPI.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ECommerce.TestBackendAPI
{
    public class OrderDetailControllerTest
    {
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IOrderDetailRepository> _orderDetailRepository;
        private readonly Mock<ICartDetailRepository> _cartDetailRepository;
        private readonly Mock<ICartRepository> _cartRepository;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly IMapper _mapper;
        private readonly OrderDetailController _orderDetailController;


        // Initialization
        public OrderDetailControllerTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new mapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _orderRepository = new Mock<IOrderRepository>();
            _orderDetailRepository = new Mock<IOrderDetailRepository>();
            _cartRepository = new Mock<ICartRepository>();
            _cartDetailRepository = new Mock<ICartDetailRepository>();
            _productRepository = new Mock<IProductRepository>();
            _orderDetailController = new OrderDetailController(_orderRepository.Object, _orderDetailRepository.Object, _cartDetailRepository.Object, _cartRepository.Object, _productRepository.Object, _mapper);
        }


        // Test methods
        [Fact]
        public async void GetAllOrderDetailByOrder_WithParams_Ok_ListShowedOrderDetailDTO()
        {
            // Arrange
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            // Setup for MockOrderRepository
            Order order = MockData_OrderDetail.GetListOrder().ElementAt(0);
            _orderRepository.Setup(_ => _.GetOrder(userId)).ReturnsAsync(order);
            // Setup for MockOrderDetailRepository
            List<OrderDetail> orderDetails = MockData_OrderDetail.GetListOrderDetail(order);
            _orderDetailRepository.Setup(_ => _.GetOrderDetail(order)).ReturnsAsync(orderDetails);
            // Setup for MockProductRepository
            for(int i=0; i<orderDetails.Count; i++)
            {
                _productRepository.Setup(_ => _.GetProductById(orderDetails[i].Id)).ReturnsAsync(orderDetails[i].product);
            }

            // Act
            var actionResult = await _orderDetailController.GetAllOrderDetailByOrder(userId);
            var okActionResult = actionResult.Result as OkObjectResult;
            List<ShowedOrderDetailDTO> data = okActionResult.Value as List<ShowedOrderDetailDTO>;

            // Assert
            Assert.NotNull(data);
            // I create 2 OrderDetail and it will give-back 2 Product. So, from 2 Product, I hope it will return 2 ShowedOrderDetailDTO
            Assert.Equal(2, data?.Count);
        }


        [Fact]
        public async void GetOrderDetail_WithParams_Ok_ShowedOrderDetailDTO()
        {
            // Arrange
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            int orderDetailId = 1;
            // Setup OrderDetailRepository
            Order order = MockData_OrderDetail.GetListOrder().ElementAt(0);
            OrderDetail orderDetail = MockData_OrderDetail.GetListOrderDetail(order).ElementAt(0);
            _orderDetailRepository.Setup(_ => _.GetOrderDetail(orderDetailId)).ReturnsAsync(orderDetail);
            // Setup ProductRepository
            Product product = orderDetail.product;
            _productRepository.Setup(_ => _.GetProductById(orderDetail.productId)).ReturnsAsync(product);

            // Act
            var actionResult = await _orderDetailController.GetOrderDetail(userId, orderDetailId);
            var okActionResult = actionResult.Result as OkObjectResult;
            ShowedOrderDetailDTO data = okActionResult.Value as ShowedOrderDetailDTO;

            // Assert
            Assert.NotNull(data);
            Assert.Equal(data.Id, orderDetail.Id);
            Assert.Equal(data.showedProductDTO.id, product.Id);
            Assert.Equal(data.showedProductDTO.productName, product.productName);
        }


        [Fact]
        public async void CreateOrderDetail_WithParams_Ok_String()
        {
            // Arrange
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            List<OrderDetailDTO> orderDetailDTOs = MockData_OrderDetail.GetListOrderDetailDTO();
            // Setup for OrderRepository
            Order order = MockData_OrderDetail.GetListOrder().ElementAt(0);
            _orderRepository.Setup(_ => _.GetOrder(userId)).ReturnsAsync(order);
            // Setup for CartRepository
            Cart cart = MockData_CartDetail.GetListCart().ElementAt(0);
            _cartRepository.Setup(_ => _.GetCart(userId)).ReturnsAsync(cart);
            // Setup for ProductRepository and CartDetailRepository
            List<CartDetail> cartDetails = MockData_CartDetail.GetListCartDetail(cart);
            for(int i=0; i<orderDetailDTOs.Count; i++)
            {
                // Here, I setted up that:
                // + In orderDetailDTOs, there are only 2 orderDetailDTO: { productId: 1, number: 1 } and { productId: 2, number: 2 }
                // + In cartDetails, there are also only 2 CartDetail:
                //   { Id: 1, ... , product: { productId: 1, ...}, ... , number: 1 }
                //   { Id: 2, ... , product: { productId: 2, ...}, ... , number: 2 }
                // So It's Ok to think that all of orderDetailDTOs are already in cartDetails
                _productRepository.Setup(_ => _.GetProductById(orderDetailDTOs[i].productId)).ReturnsAsync(cartDetails[i].product);
                _cartDetailRepository.Setup(_ => _.GetCartDetail(cart.Id, orderDetailDTOs[i].productId)).ReturnsAsync(cartDetails[i]);
            }

            // Act
            var actionResult = await _orderDetailController.CreateOrderDetail(userId, orderDetailDTOs);
            var okActionResult = actionResult as OkObjectResult;
            var badrequestActionResult = actionResult as BadRequestObjectResult;
            string okData = okActionResult.Value as string;
            string badrequestData = badrequestActionResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Null(badrequestData);
            // Attention: If I change the return string inside this logic, this test is failed
            Assert.Equal(okData, "Order sucessfully!");
        }


        [Fact]
        public async void UpdateOrderDetail_WithParams_Ok_String()
        {
            // Arrange
            string userId = "38cd5450-4071-453d-b146-5940453bbe50";
            int orderDetailId = 1;
            ReviewOrderDetailDTO reviewOrderDetailDTO = new ReviewOrderDetailDTO
            {
                comment = "This is just a test comment",
                rating = 5
            };
            // Setup for OrderDetailRepository
            Order order = MockData_OrderDetail.GetListOrder().ElementAt(0);
            OrderDetail orderDetail = MockData_OrderDetail.GetListOrderDetail(order).ElementAt(0);
            _orderDetailRepository.Setup(_ => _.GetOrderDetail(orderDetailId)).ReturnsAsync(orderDetail);
            //_orderDetailRepository.Setup(_ => _.UpdateOrderDetail(orderDetail));

            // Act
            var actionResult = await _orderDetailController.UpdateOrderDetail(userId, orderDetailId, reviewOrderDetailDTO);
            var okActionResult = actionResult as OkObjectResult;
            var badrequestResult = actionResult as BadRequestObjectResult;
            string okData = okActionResult.Value as string;
            string badrequestData = badrequestResult?.Value as string;

            // Assert
            Assert.NotNull(okData);
            Assert.Null(badrequestData);
            Assert.Equal(okData, "Update OrderDetail sucessfully");
        }
    }
}
