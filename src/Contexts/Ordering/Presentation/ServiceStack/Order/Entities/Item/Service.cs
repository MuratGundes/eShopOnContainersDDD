﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using NServiceBus;
using ServiceStack;

namespace eShop.Ordering.Order.Entities.Item
{
    public class Service : ServiceStack.Service
    {
        private readonly IMessageSession _bus;

        public Service(IMessageSession bus)
        {
            _bus = bus;
        }

        public Task<object> Any(Services.ListOrderItems request)
        {
            return _bus.RequestPaged<Queries.Items, Models.OrderingOrderItem>(new Queries.Items
            {
                OrderId = request.OrderId
            });
        }

        public Task Any(Services.AddOrderItem request)
        {
            return _bus.CommandToDomain(new Commands.Add
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            });
        }

        public Task Any(Services.OverridePriceOrderItem request)
        {
            return _bus.CommandToDomain(new Commands.OverridePrice
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Price = request.Price
            });
        }

        public Task Any(Services.RemoveOrderItem request)
        {
            return _bus.CommandToDomain(new Commands.Remove
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
            });
        }
    }
}
