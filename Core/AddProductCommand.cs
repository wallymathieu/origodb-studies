using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using With;
using With.ReadonlyEnumerable;
namespace SomeBasicOrigoDbApp.Core
{

	[Serializable]
	public class AddProductCommand : ImmutabilityCommand<Models>
	{
		public virtual float Cost { get; set; }

		public virtual string Name { get; set; }

		public virtual int Id { get; set; }

		public virtual int Version { get; set; }

		public override void Execute(Models model, out Models newModel)
		{
			var product = new Product(id: Id, cost: Cost, name: Name, version: Version);
			newModel = model.With(m => m.Products.Add(product));
		}
	}

}
