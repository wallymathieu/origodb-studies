using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using SomeBasicOrigoDbApp.Core;
using OrigoDB.Core;
using OrigoDB.Core.Test;
using System.Linq;
using System;
using With;
using OrigoDB.Core.Configuration;
using System.Globalization;

namespace SomeBasicOrigoDbApp.Tests
{
	[TestFixture]
	public class CustomerDataTests
	{

		private LocalEngineClient<Models> _engine;

		[Test]
		public void CanGetCustomerById()
		{
			var customer = _engine.Execute(new GetCustomer { Id = 1 });

			Assert.IsNotNull(customer);
		}

		[Test]
		public void CanGetCustomerByFirstname()
		{
			var customers = _engine.Execute(m=>
				m.QueryOverCustomers()
				.Where(c => c.Firstname == "Steve")
				.ToList());
			Assert.AreEqual(3, customers.Count);
		}

		[Test]
		public void CanGetProductById()
		{
			var product = _engine.Execute(m=>m.GetProduct(1));

			Assert.IsNotNull(product);
		}

		[Test]
		public void OrderContainsProduct()
		{
			var order = _engine.Execute(m=>m.GetOrder(1));
			Assert.True(order.Products.Any(p => p.Id == 1));
		}
		[Test]
		public void OrderHasACustomer()
		{
			Assert.IsNotNullOrEmpty(_engine.Execute(m=>m.GetTheCustomerForOrder(1)).Firstname);
		}


		[TestFixtureSetUp]
		public void TestFixtureSetup()
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
			var enUs = CultureInfo.GetCultureInfo("en-US");
            import.Parse("Product", entity=>
				
				_engine.Execute(new AddProductCommand
				{ 
					Cost=float.Parse(entity.Cost, enUs),
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

		public EngineConfiguration CreateConfig()
		{
			return EngineConfiguration
				.Create().ForIsolatedTest()
                .Tap(c=>c.Kernel = Kernels.Immutability)
                .Tap(c=>c.Synchronization = SynchronizationMode.None);
		}


		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			Config.Engines.CloseAll();
			//_sessionFactory.Dispose();
		}
	}
}
