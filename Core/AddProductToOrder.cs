using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using With;
using With.ReadonlyEnumerable;

namespace SomeBasicOrigoDbApp.Core
{

    [Serializable]
    public class AddProductToOrder : ImmutabilityCommand<Models>
    {
        public readonly int OrderId;
        public readonly int ProductId;

        public AddProductToOrder(int orderId, int productId)
        {
            OrderId = orderId;
            ProductId = productId;
        }

        public override void Execute(Models model, out Models newModel)
        {
            var newOrder = 
                model.GetOrder(OrderId)
                        .With(o => o.Products.Add(model.GetProduct(ProductId)));

            newModel = model
                .With(m => m.Orders.Replace(newOrder.Id, newOrder));
        }
    }
}
