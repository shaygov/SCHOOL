using System;
using System.Collections.Generic;

namespace DelegatesAndEventsExample.Store
{
    public class ReportGenerator
    {
        private List<string> reports;
        
        public ReportGenerator()
        {
            reports = new List<string>();
        }
        
        public void OnProductAdded(object sender, Product product)
        {
            string report = $"[{DateTime.Now:HH:mm:ss}] Добавен: {product.Name} в категория {product.Category}";
            reports.Add(report);
        }
        
        public void OnProductSold(object sender, Product product)
        {
            string report = $"[{DateTime.Now:HH:mm:ss}] Продаден: {product.Name} (останали: {product.Stock})";
            reports.Add(report);
        }
        
        public void OnCategoryAdded(object sender, string category)
        {
            string report = $"[{DateTime.Now:HH:mm:ss}] Нова категория: {category}";
            reports.Add(report);
        }
        
        public void GenerateReport()
        {
            Console.WriteLine("\n=== Дневен отчет ===");
            foreach (var report in reports)
            {
                Console.WriteLine(report);
            }
        }
    }
}
