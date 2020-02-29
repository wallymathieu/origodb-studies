using System.IO;
using System.Xml.Linq;
using SomeBasicOrigoDbApp.Core;
using OrigoDB.Core;
using OrigoDB.Core.Test;
using System.Linq;
using System;
using Xunit;

namespace SomeBasicOrigoDbApp.Tests
{
	public class CustomerDataTests
	{

		private static LocalEngineClient<Models> _engine;

		[Fact]
		public void CanGetCustomerById()
		{
			var customer = _engine.Execute(new GetCustomer { Id = 1 });

			Assert.NotNull(customer);
		}

		[Fact]
		public void CanGetCustomerByFirstname()
		{
			var customers = _engine.Execute(m=>
				m.QueryOverCustomers()
				.Where(c => c.Firstname == "Steve")
				.ToList());
			Assert.Equal(3, customers.Count);
		}

		[Fact]
		public void CanGetProductById()
		{
			var product = _engine.Execute(m=>m.GetProduct(1));

			Assert.NotNull(product);
		}

		[Fact]
		public void OrderContainsProduct()
		{
			var order = _engine.Execute(m=>m.GetOrder(1));
			Assert.True(order.Products.Any(p => p.Id == 1));
		}
		[Fact]
		public void OrderHasACustomer()
		{
			Assert.False(string.IsNullOrEmpty( _engine.Execute(m=>m.GetTheCustomerForOrder(1)).Firstname));
		}


		static CustomerDataTests()
		{
			_engine =(LocalEngineClient<Models>) Engine.For<Models>(CreateConfig());
			var import = new XmlImport(XDocument.Load(Path.Combine("TestData", "TestData.xml")), "http://tempuri.org/Database.xsd");
	
			import.Parse("Customer", entity=>
				
				_engine.Execute(new AddCustomerCommand
				{ 
					Id=int.Parse(entity.Id),
					Firstname = entity.Firstname,
					Lastname = entity.Lastname,
					Version = int.Parse(entity.Version)
				})
			);

			import.Parse("Order", entity=>
				
				_engine.Execute(new AddOrderCommand
				{ 
					Customer=int.Parse(entity.Customer),
					Id=int.Parse(entity.Id),
					OrderDate = DateTime.Parse( entity.OrderDate)
				})
			);
			import.Parse("Product", entity=>
				
				_engine.Execute(new AddProductCommand
				{ 
					Cost=float.Parse(entity.Cost),
					Id=int.Parse(entity.Id),
					Version = int.Parse( entity.Version),
					Name = entity.Name
				})
			);

			import.Parse("OrderProduct", entity=>
			
				_engine.Execute(new AddProductToOrder { 
					ProductId = int.Parse( entity.Product), 
					OrderId = int.Parse(entity.Order) 
				})
			);
		}

		static EngineConfiguration CreateConfig()
		{
			return new EngineConfiguration().ForIsolatedTest();
		}


		public void TestFixtureTearDown()
		{
			Config.Engines.CloseAll();
			//_sessionFactory.Dispose();
		}
	}
}
