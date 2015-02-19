using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using SomeBasicOrigoDbApp.Core;
using OrigoDB.Core;
using OrigoDB.Core.Test;
using System.Linq;

namespace SomeBasicOrigoDbApp.Tests
{
	[TestFixture]
	public class CustomerDataTests
	{

		private LocalEngineClient<Models> _engine;

		[Test]
		public void CanGetCustomerById()
		{
			var customer = _engine.Execute(new Get<Customer> { Id = 1 });

			Assert.IsNotNull(customer);
		}

		[Test]
		public void CanGetCustomerByFirstname()
		{
			var customers = _engine.Execute(m=>
				m.QueryOver<Customer>()
				.Where(c => c.Firstname == "Steve")
				.ToList());
			Assert.AreEqual(3, customers.Count);
		}

		[Test]
		public void CanGetProductById()
		{
			var product = _engine.Execute(m=>m.Get<Product>(1));

			Assert.IsNotNull(product);
		}

		[SetUp]
		public void Setup()
		{
		}


		[TearDown]
		public void TearDown()
		{
		}

		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			_engine =(LocalEngineClient<Models>) Engine.For<Models>(CreateConfig());
			XmlImport.Parse(XDocument.Load(Path.Combine("TestData", "TestData.xml")), new[] { typeof(Customer), typeof(Order), typeof(Product) },
							(type, obj) => _engine.Execute(new AddCommand { Object = obj }), "http://tempuri.org/Database.xsd");
		}

		public EngineConfiguration CreateConfig()
		{
			return EngineConfiguration
				.Create().ForIsolatedTest();
		}


		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			Config.Engines.CloseAll();
			//_sessionFactory.Dispose();
		}
	}
}
