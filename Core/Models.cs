using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeBasicOrigoDbApp.Core
{
	[Serializable]
	public class Models : Model
	{
		private IDictionary<long, Customer> _customers = new Dictionary<long, Customer>();
		private IDictionary<long, Product> _products = new Dictionary<long, Product>();
		private IDictionary<long, Order> _orders = new Dictionary<long, Order>();
		public void Save(Customer obj) 
		{
			_customers[obj.Id] = obj;
		}
		public void Save(Product obj)
		{
			_products[obj.Id] = obj;
		}
		public void Save(Order obj)
		{
			_orders[obj.Id] = obj;
		}
		public IEnumerable<Customer> QueryOverCustomers()
		{
			return _customers.Values;
		}

		public Customer GetCustomer(int v) 
		{
			return QueryOverCustomers().SingleOrDefault(t => t.Id == v);
		}

		public IEnumerable<Product> QueryOverProducts()
		{
			return _products.Values;
		}

		public Product GetProduct(int v)
		{
			return QueryOverProducts().SingleOrDefault(t => t.Id == v);
		}

		public Order GetOrder(int v)
		{
			return _orders.Values.SingleOrDefault(t=>t.Id == v);
		}

		public Customer GetTheCustomerForOrder(int v)
		{
			return GetOrder(v).Customer;
		}
	}

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

	[Serializable]
	public class GetCustomer : Query<Models, Customer> 
	{
		public int Id { get; set; }

		public override Customer Execute(Models model) 
		{
			return model.GetCustomer(Id);
		}
	}

	[Serializable]
	public class AddProductToOrder : Command<Models>
	{
		public int OrderId{get;set;}
		public int ProductId{get;set;}
		
		public override void Execute(Models model)
		{
			model.GetOrder(OrderId).Products.Add(model.GetProduct(ProductId));
		}
	}

}
