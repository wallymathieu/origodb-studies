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
				    ( 
					id:int.Parse(entity.Id),
					firstName: entity.Firstname,
					lastName: entity.Lastname,
					version: int.Parse(entity.Version)
                    ))
			);

			import.Parse("Order", entity=>
				
				_engine.Execute(new AddOrderCommand
				    ( 
					customer:int.Parse(entity.Customer),
					id:int.Parse(entity.Id),
					orderDate: DateTime.Parse( entity.OrderDate),
                    version: int.Parse( entity.Version)                      
                    ))
			);
			var enUs = CultureInfo.GetCultureInfo("en-US");
            import.Parse("Product", entity=>
				
				_engine.Execute(new AddProductCommand
				(
					cost:float.Parse(entity.Cost, enUs),
					id:int.Parse(entity.Id),
					version: int.Parse( entity.Version),
					name: entity.Name
                    ))
			);

			import.Parse("OrderProduct", entity=>
			
				_engine.Execute(new AddProductToOrder
                ( 
					productId: int.Parse( entity.Product), 
                    orderId: int.Parse(entity.Order) 
                ))
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
