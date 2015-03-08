using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeBasicOrigoDbApp.Core
{

	[Serializable]
	public class AddProductCommand : Command<Models>
	{
        public virtual float Cost { get; set; }

        public virtual string Name { get; set; }

        public virtual int Id { get; set; }

        public virtual int Version { get; set; }

		public override void Execute(Models model)
		{
			model.Save(new Product{ Id=Id, Cost=Cost, Name=Name, Version=Version });
		}
	}

}
