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

		public override void Execute(Models model)
		{
            model.Save(new Order{ Customer=model.GetCustomer(Customer), OrderDate=OrderDate,Id=Id} );
		}
	}

}
