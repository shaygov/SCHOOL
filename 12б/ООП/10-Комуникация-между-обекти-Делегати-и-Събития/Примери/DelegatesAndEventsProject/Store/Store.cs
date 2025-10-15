using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegatesAndEventsExample.Store
{
    public class Store
    {
        private List<Product> products;
        private Dictionary<string, int> sales;
        
        public event EventHandler<Product> ProductAdded;
        public event EventHandler<Product> ProductSold;
        public event EventHandler<Product> LowStockWarning;
        public event EventHandler<string> CategoryAdded;
        
        public Store()
        {
            products = new List<Product>();
            sales = new Dictionary<string, int>();
        }
        
        public void AddProduct(Product product)
        {
            products.Add(product);
            ProductAdded?.Invoke(this, product);
            
            if (!sales.ContainsKey(product.Category))
            {
                sales[product.Category] = 0;
                CategoryAdded?.Invoke(this, product.Category);
            }
        }
        
        public bool SellProduct(string productName, int quantity)
        {
            var product = products.FirstOrDefault(p => p.Name == productName);
            if (product != null && product.Stock >= quantity)
            {
                product.Stock -= quantity;
                sales[product.Category] += quantity;
                
                ProductSold?.Invoke(this, product);
                
                if (product.Stock < 5)
                {
                    LowStockWarning?.Invoke(this, product);
                }
                
                return true;
            }
            return false;
        }
        
        public List<Product> GetProducts()
        {
            return new List<Product>(products);
        }
        
        public Dictionary<string, int> GetSales()
        {
            return new Dictionary<string, int>(sales);
        }
    }
}
