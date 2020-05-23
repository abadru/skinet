using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepo;

        public OrderService(IUnitOfWork unitOfWork,
            IBasketRepository basketRepo
        )
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreatOrderAsync(string buyerEmail, int deliveryMethodId, string basketId,
            Address shippingAddress)
        {
            // Get Basket

            var basket = await _basketRepo.GetBasketAsync(basketId);

            // Get Items from product repo

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);

                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);

                items.Add(orderItem);
            }

            // Get Delivery Method

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // Calculate Subtotal

            var subtotal = items.Sum(x => x.Price * x.Quantity);

            // Create the order

            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);

            // Save the order
            _unitOfWork.Repository<Order>().Add(order);
            var result = await _unitOfWork.Complete();

            if(result <= 0)
                return null;

            
            //Delete basket
            await _basketRepo.DeleteBasketAsync(basketId);
            
            // Return the created order

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification( orderId, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }
    }
}