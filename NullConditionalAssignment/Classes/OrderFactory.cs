using NullConditionalAssignment.Models;

namespace NullConditionalAssignment.Classes;

public static class OrderFactory
{
    public static Order CreateOrder()
    {
        var order = new Order
        {
                Id = 1,
                OrderItems =
                [
                    new OrderItem
                    {
                        OrderItemId = 1,
                        OrderId = 1,
                        ProductId = 101,
                        Product = new Product
                        {
                            Id = 101,
                            Name = "Keyboard"
                        },
                        Quantity = 1,
                        UnitPrice = 49.99m
                    },
                    new OrderItem 
                    {
                        OrderItemId = 2,
                        OrderId = 1,
                        ProductId = 102,
                        Product = new Product
                        { 
                            Id = 102,
                            Name = "Mouse"
                        },
                        Quantity = 2,
                        UnitPrice = 24.95m
                    },
                    new OrderItem
                    {
                        OrderItemId = 3,
                        OrderId = 1,
                        ProductId = 103,
                        Quantity = 2,
                        UnitPrice = 229.99m
                    },
                    new OrderItem()
                    {
                        OrderItemId = 4,
                        OrderId = 1,
                        ProductId = 104,
                        Product = new Product { Id = 104, Name = "Webcam" },
                        Quantity = 1,
                        UnitPrice = 75.50m
                    }

                ]
            };

            foreach (var orderItem in order.OrderItems)
            {
                orderItem.Order = order;
            }


            return order;
        }
    }




