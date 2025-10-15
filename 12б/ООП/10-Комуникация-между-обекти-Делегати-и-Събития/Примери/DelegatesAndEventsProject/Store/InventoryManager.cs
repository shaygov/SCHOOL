using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegatesAndEventsExample.Store
{
    public class InventoryManager
    {
        private List<Product> lowStockProducts;
        
        public InventoryManager()
        {
            lowStockProducts = new List<Product>();
        }
        
        public void OnLowStockWarning(object sender, Product product)
        {
            if (!lowStockProducts.Contains(product))
            {
                lowStockProducts.Add(product);
            }
            Console.WriteLine($"Инвентар: ПРЕДУПРЕЖДЕНИЕ за ниски наличности - {product}");
        }
        
        public void OnProductSold(object sender, Product product)
        {
            if (product.Stock >= 5 && lowStockProducts.Contains(product))
            {
                lowStockProducts.Remove(product);
                Console.WriteLine($"Инвентар: Продуктът {product.Name} вече не е с ниски наличности");
            }
        }
        
        public void DisplayLowStockProducts()
        {
            Console.WriteLine("\n=== Продукти с ниски наличности ===");
            if (lowStockProducts.Count == 0)
            {
                Console.WriteLine("Няма продукти с ниски наличности");
            }
            else
            {
                foreach (var product in lowStockProducts)
                {
                    Console.WriteLine(product);
                }
            }
        }
    }
}
