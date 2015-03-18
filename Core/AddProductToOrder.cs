using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using With;
using With.ReadonlyEnumerable;

namespace SomeBasicOrigoDbApp.Core
{

	[Serializable]
	public class AddProductToOrder : Command<Models>
	{
		public int OrderId{get;set;}
		public int ProductId{get;set;}
		
		public override void Execute(Models model)
		{
            model.Save( model.GetOrder(OrderId)
                .With(o=>o.Products.Add(model.GetProduct(ProductId))) );
		}
	}

}
