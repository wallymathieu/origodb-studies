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
		private IDictionary<long, IList<Customer>> _objects = new Dictionary<long, IList<Customer>>();
		private IDictionary<long, IList<Product>> _objects2 = new Dictionary<long, IList<Product>>();
		private IDictionary<long, IList<Order>> _objects3 = new Dictionary<long, IList<Order>>();
		public void Save(Customer obj) 
		{
			var t = obj.GetType();
            if (!_objects.ContainsKey(obj.Id))
			{
				_objects[obj.Id] = new List<Customer>();
			}
			_objects[obj.Id].Add(obj);
		}
		public void Save(Product obj)
		{
			var t = obj.GetType();
			if (!_objects2.ContainsKey(obj.Id))
			{
				_objects2[obj.Id] = new List<Product>();
			}
			_objects2[obj.Id].Add(obj);
		}
		public void Save(Order obj)
		{
			var t = obj.GetType();
			if (!_objects3.ContainsKey(obj.Id))
			{
				_objects3[obj.Id] = new List<Order>();
			}
			_objects3[obj.Id].Add(obj);
		}
		public IEnumerable<Customer> QueryOverCustomers()
		{
			return _objects.Values.Select(v=>v.Last());
		}

		public Customer GetCustomer(int v) 
		{
			return QueryOverCustomers().SingleOrDefault(t => t.Id == v);
		}
		public IEnumerable<Product> QueryOverProducts()
		{
			return _objects2.Values.Select(v => v.Last());
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
