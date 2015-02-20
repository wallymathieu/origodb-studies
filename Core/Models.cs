using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeBasicOrigoDbApp.Core
{
	[Serializable]
	public class Models : Model
	{
		private IDictionary<long, Customer> _objects = new Dictionary<long, Customer>();
		private IDictionary<long, Product> _objects2 = new Dictionary<long, Product>();
		private IDictionary<long, Order> _objects3 = new Dictionary<long, Order>();
		public void Save(Customer obj) 
		{
			_objects[obj.Id] = obj;
		}
		public void Save(Product obj)
		{
			_objects2[obj.Id] = obj;
		}
		public void Save(Order obj)
		{
			_objects3[obj.Id] = obj;
		}
		public IEnumerable<Customer> QueryOverCustomers()
		{
			return _objects.Values;
		}

		public Customer GetCustomer(int v) 
		{
			return QueryOverCustomers().SingleOrDefault(t => t.Id == v);
		}
		public IEnumerable<Product> QueryOverProducts()
		{
			return _objects2.Values;
		}

		public Product GetProduct(int v)
		{
			return QueryOverProducts().SingleOrDefault(t => t.Id == v);
		}
	}

	[Serializable]
	public class AddCustomerCommand : Command<Models> 
	{
		public Customer Object { get; set; }

		public override void Execute(Models model)
		{
			model.Save(Object);
		}
	}
	[Serializable]
	public class AddProductCommand : Command<Models>
	{
		public Product Object { get; set; }

		public override void Execute(Models model)
		{
			model.Save(Object);
		}
	}
	[Serializable]
	public class AddOrderCommand : Command<Models>
	{
		public Order Object { get; set; }

		public override void Execute(Models model)
		{
			model.Save(Object);
		}
	}

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
