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
		public virtual int Customer { get; set; }

		public virtual DateTime OrderDate { get; set; }

		public virtual int Id { get; set; }

		public virtual int Version { get; set; }

		public override void Execute(Models model, out Models newModels)
		{
			var order = new Order(customer: Customer, orderDate: OrderDate, id: Id, version: Version, products: new Product[0]);
			newModels = model.With(m => m.Orders.Add(order));
		}
	}

}
