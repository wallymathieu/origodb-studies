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
			var product = model.GetProduct(ProductId);
            var newOrder = order.With(o => o.Products.Add(product));
			newModel = model
				.With(m => m.Orders.Remove(order))
				.With(m => m.Orders.Add(newOrder));
		}
	}

}
