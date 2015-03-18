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
		public int OrderId { get; set; }
		public int ProductId { get; set; }

		public override void Execute(Models model, out Models newModel)
		{
			var order = model.GetOrder(OrderId);
			var newOrder = order.With(o => o.Products.Add(model.GetProduct(ProductId)));
			newModel = model
				.With(m => m.Orders.Remove(order))
				.With(m => m.Orders.Add(newOrder));
		}
	}

}
