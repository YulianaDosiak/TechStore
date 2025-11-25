using System;
using System.Collections.Generic;
using System.Linq;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.BLL.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDAL _orderDal;
        private readonly IOrderItemDAL _orderItemDal;
        private readonly IProductDAL _productDal;

        public OrderService(IOrderDAL orderDal, IOrderItemDAL orderItemDal, IProductDAL productDal)
        {
            _orderDal = orderDal;
            _orderItemDal = orderItemDal;
            _productDal = productDal;
        }

        public void CreateOrder(int userId, List<CartItems> items)
        {
            if (items == null || !items.Any()) return;

            decimal totalAmount = 0;

            foreach (var item in items)
            {
                var product = _productDal.GetById(item.ProductID);
                if (product != null)
                {
                    totalAmount += product.Price * item.Quantity;
                }
            }

            var newOrder = new Order
            {
                UserID = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount
            };

            var createdOrder = _orderDal.Create(newOrder);

            foreach (var item in items)
            {
                var product = _productDal.GetById(item.ProductID);
                if (product != null)
                {
                    _orderItemDal.Create(new OrderItem
                    {
                        OrderID = createdOrder.OrderID,
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        Price = product.Price 
                    });
                }
            }
        }

        public List<Order> GetUserOrders(int userId)
        {
            return _orderDal.GetAll().Where(o => o.UserID == userId).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _orderDal.GetById(orderId);
        }
    }
}