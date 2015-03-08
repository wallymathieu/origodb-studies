using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeBasicOrigoDbApp.Core
{

	[Serializable]
	public class AddCustomerCommand : Command<Models> 
	{

        public virtual int Id { get; set; }

        public virtual string Firstname { get; set; }

        public virtual string Lastname { get; set; }

		public virtual int Version { get; set; }

		public override void Execute(Models model)
		{
			model.Save(new Customer{ Id = Id, Firstname = Firstname, Lastname = Lastname, Version = Version });
		}
	}

}
