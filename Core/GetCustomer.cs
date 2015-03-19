using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeBasicOrigoDbApp.Core
{

    [Serializable]
    public class GetCustomer : Query<Models, Customer> 
    {
        public int Id { get; set; }

        public override Customer Execute(Models model) 
        {
            return model.GetCustomer(Id);
        }
    }

}
