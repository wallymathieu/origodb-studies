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
        public readonly int Id;
        public readonly string Firstname;
        public readonly string Lastname;
        public readonly int Version;
        public AddCustomerCommand(int id, string firstName, string lastName, int version)
        {
            Id = id;
            Firstname = firstName;
            Lastname = lastName;
            Version = version;
        }

        public override void Execute(Models model, out Models newModel)
        {
            var customer = new Customer(id: Id, firstName: Firstname, lastName: Lastname, version: Version);
            newModel = model.With(m=>m.Customers.Add(customer.Id, customer));
        }
    }
}
