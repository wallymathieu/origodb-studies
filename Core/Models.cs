using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SomeBasicOrigoDbApp.Core
{
    [Serializable]
    public class Models : Model
    {
        public Models()
            : this(
                  new ReadOnlyDictionary<int,Customer>(new Dictionary<int,Customer>()),
                  new ReadOnlyDictionary<int, Product>(new Dictionary<int, Product>()),
                  new ReadOnlyDictionary<int, Order>(new Dictionary<int, Order>())
                  )
        {
        }
        public Models(IReadOnlyDictionary<int, Customer> customers, IReadOnlyDictionary<int, Product> products, IReadOnlyDictionary<int, Order> orders)
        {
            Customers = customers;
            Products = products;
            Orders = orders;
        }

        public readonly IReadOnlyDictionary<int, Customer> Customers;
        public readonly IReadOnlyDictionary<int, Product> Products;
        public readonly IReadOnlyDictionary<int,Order> Orders;

        public IEnumerable<Customer> QueryOverCustomers()
        {
            return Customers.Values;
        }

        public Customer GetCustomer(int v)
        {
            return Customers[v];
        }

        public IEnumerable<Product> QueryOverProducts()
        {
            return Products.Values;
        }

        public Product GetProduct(int v)
        {
            return Products[v];
        }

        public IEnumerable<Order> QueryOverOrders()
        {
            return Orders.Values;
        }

        public Order GetOrder(int v)
        {
            return Orders[v];
        }

        public Customer GetTheCustomerForOrder(int v)
        {
            return GetCustomer(GetOrder(v).Customer);
        }
    }
}
