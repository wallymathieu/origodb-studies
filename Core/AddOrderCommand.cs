using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeBasicOrigoDbApp.Core
{

	[Serializable]
	public class AddOrderCommand : Command<Models>
	{
        public virtual int Customer { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual int Id { get; set; }

        public virtual int Version { get; set; }

        public override void Execute(Models model)
		{
            model.Save(new Order(customer:Customer, orderDate:OrderDate,id:Id, version: Version,products:new Product[0]) );
		}
	}

}
