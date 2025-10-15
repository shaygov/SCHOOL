using System;
using System.Collections.Generic;

namespace DelegatesAndEventsExample.Store
{
    public class StatisticsManager
    {
        private int totalProducts;
        private int totalSales;
        private Dictionary<string, int> categorySales;
        
        public StatisticsManager()
        {
            totalProducts = 0;
            totalSales = 0;
            categorySales = new Dictionary<string, int>();
        }
        
        public void OnProductAdded(object sender, Product product)
        {
            totalProducts++;
            Console.WriteLine($"Статистика: Добавен продукт. Общо: {totalProducts}");
        }
        
        public void OnProductSold(object sender, Product product)
        {
            totalSales++;
            Console.WriteLine($"Статистика: Продаден продукт. Общо продажби: {totalSales}");
        }
        
        public void OnCategoryAdded(object sender, string category)
        {
            Console.WriteLine($"Статистика: Добавена нова категория: {category}");
        }
        
        public void DisplayStatistics()
        {
            Console.WriteLine("\n=== Статистика ===");
            Console.WriteLine($"Общо продукти: {totalProducts}");
            Console.WriteLine($"Общо продажби: {totalSales}");
        }
    }
}
