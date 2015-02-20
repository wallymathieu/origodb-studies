﻿using System;
using System.Collections.Generic;

namespace SomeBasicOrigoDbApp.Core
{
	[Serializable]
	public class Order 
    {
        public virtual Customer Customer { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual int Id { get; set; }

        public virtual IList<Product> Products { get; set; }

        public virtual int Version { get; set; }

    }
}
