using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Data
{
    class OrderData
    {
        public static List<Order> Orders { get; private set; }
        public static List<Order> UserOrders { get; private set; }
        public static List<OrdersGroup> GroupedOrders { get; private set; }

        static OrderData()
        {
            Orders = new List<Order>();
        }

        public static async Task AddToOrderAsync(BasketProduct product)
        {
            //convert product to order
            Order order = new Order(product.Id, product.Name, product.Description, product.Price, product.ImageUrl, product.Stock, 1);

            WebDataService webDataService = new WebDataService();
            await webDataService.PostAddOrder(product.Id, product.Quantity);

            ////add to orderdata list (change to api call in future)
            //Orders.Add(order);
        }
        public static void Clear()
        {
            Orders.Clear();
        }

        public static async Task<List<OrdersGroup>> LoadOrdersAsync()
        {
            WebDataService webDataService = new WebDataService();
            Orders = await webDataService.GetOrders();
            //UserOrders = new List<Order>();
            bool isGrouped = false;
            GroupedOrders = new List<OrdersGroup>();
            foreach (var order in Orders)
            {
                isGrouped = false;
                if(GroupedOrders.Count == 0)
                {
                    OrdersGroup tempGroup = new OrdersGroup();
                    List<Order> tempOrderList = new List<Order>();
                    tempGroup.OrderDate = order.OrderDate;
                    tempOrderList.Add(order);
                    tempGroup.theOrders = tempOrderList;
                    tempGroup.Total = order.getTotal();
                    GroupedOrders.Add(tempGroup);
                    isGrouped = true;
                }
                else if(GroupedOrders.Count > 0)
                {
                    foreach(var group in GroupedOrders)
                    {
                        if(group.OrderDate.Date == order.OrderDate.Date)
                        {
                            group.theOrders.Add(order);
                            group.Total += order.getTotal();
                            isGrouped = true;
                        }
                    }
                    if(isGrouped == false)
                    {
                        OrdersGroup tempGroup = new OrdersGroup();
                        tempGroup.OrderDate = order.OrderDate;
                        //tempGroup.theOrders.Add(order);
                        List<Order> tempOrderList = new List<Order>();
                        tempOrderList.Add(order);
                        tempGroup.theOrders = tempOrderList;
                        tempGroup.Total = order.getTotal();
                        GroupedOrders.Add(tempGroup);
                    }
                }
            }
            return GroupedOrders;
        }
        public static List<Order> loadOrdersByDate(DateTime requestedDate)
        {
            List<Order> requestedOrder = new List<Order>();
            foreach (var order in Orders)
            {
                DateTime a = order.OrderDate.Date;
                if (a == requestedDate.Date)
                {
                    requestedOrder.Add(order);
                }
            }
            return requestedOrder;
        }
        public static void toggleBtn(Order selectedOrder)
        {
            foreach (var order in Orders)
            {
                if (order == selectedOrder)
                {
                    order.displayBtn = !(order.displayBtn);
                }
            }
        }
        public static void removeOrder(Order order)
        {
            Orders.Remove(order);
        }
    }
}
