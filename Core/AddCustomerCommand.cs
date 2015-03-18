using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using With;
using With.ReadonlyEnumerable;
namespace SomeBasicOrigoDbApp.Core
{

	[Serializable]
	public class AddCustomerCommand : ImmutabilityCommand<Models> 
	{

        public virtual int Id { get; set; }

        public virtual string Firstname { get; set; }

        public virtual string Lastname { get; set; }

		public virtual int Version { get; set; }

		public override void Execute(Models model, out Models newModel)
		{
			var customer = new Customer(id: Id, firstName: Firstname, lastName: Lastname, version: Version);
			newModel = model.With(m=>m.Customers.Add(customer));
		}
	}
}
