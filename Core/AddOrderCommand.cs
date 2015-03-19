using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using With;
using With.ReadonlyEnumerable;
namespace SomeBasicOrigoDbApp.Core
{

    [Serializable]
    public class AddOrderCommand : ImmutabilityCommand<Models>
    {
        public readonly int Customer;
        public readonly DateTime OrderDate;
        public readonly int Id;
        public readonly int Version;

        public AddOrderCommand(int id, int customer, DateTime orderDate, int version)
        {
            Id = id;
            Customer = customer;
            OrderDate = orderDate;
            Version = version;
        }


        public override void Execute(Models model, out Models newModels)
        {
            var order = new Order(customer: Customer, orderDate: OrderDate, id: Id, version: Version, products: new Product[0]);
            newModels = model.With(m => m.Orders.Add(order));
        }
    }

}
