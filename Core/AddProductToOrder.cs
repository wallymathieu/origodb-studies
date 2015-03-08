using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeBasicOrigoDbApp.Core
{

	[Serializable]
	public class AddProductToOrder : Command<Models>
	{
		public int OrderId{get;set;}
		public int ProductId{get;set;}
		
		public override void Execute(Models model)
		{
			model.GetOrder(OrderId).Products.Add(model.GetProduct(ProductId));
		}
	}

}
