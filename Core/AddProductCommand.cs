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
        public readonly float Cost;
        public readonly string Name;
        public readonly int Id;
        public readonly int Version;

        public AddProductCommand(int id, float cost, string name, int version)
        {
            Id = id;
            Cost = cost;
            Name = name;
            Version = version;           
        }

        public override void Execute(Models model, out Models newModel)
        {
            var product = new Product(id: Id, cost: Cost, name: Name, version: Version);
            newModel = model.With(m => m.Products.Add(product));
        }
    }

}
