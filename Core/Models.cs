using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomeBasicOrigoDbApp.Core
{
	[Serializable]
	public class Models : Model
	{
		public Models()
			: this(new Customer[0], new Product[0], new Order[0])
		{
		}
		public Models(IEnumerable<Customer> customers, IEnumerable<Product> products, IEnumerable<Order> orders)
		{
			Customers = customers;
			Products = products;
			Orders = orders;
		}

		public readonly IEnumerable<Customer> Customers;
		public readonly IEnumerable<Product> Products;
		public readonly IEnumerable<Order> Orders;

		public IEnumerable<Customer> QueryOverCustomers()
		{
			return Customers;
		}

		public Customer GetCustomer(int v)
		{
			return Customers.Single(c => c.Id == v);
		}

		public IEnumerable<Product> QueryOverProducts()
		{
			return Products;
		}

		public Product GetProduct(int v)
		{
			return Products.Single(c => c.Id == v);
		}

		public IEnumerable<Order> QueryOverOrders()
		{
			return Orders;
		}

		public Order GetOrder(int v)
		{
			return Orders.Single(c => c.Id == v);
		}

		public Customer GetTheCustomerForOrder(int v)
		{
			return GetCustomer(GetOrder(v).Customer);
		}
	}
}
