using System;

namespace DelegatesAndEventsExample.Store
{
    public class StoreNotificationService
    {
        public void OnProductAdded(object sender, Product product)
        {
            Console.WriteLine($"Уведомление: Нов продукт '{product.Name}' е добавен в магазина!");
        }
        
        public void OnProductSold(object sender, Product product)
        {
            Console.WriteLine($"Уведомление: Продукт '{product.Name}' е продаден!");
        }
        
        public void OnLowStockWarning(object sender, Product product)
        {
            Console.WriteLine($"Уведомление: Продукт '{product.Name}' има ниски наличности ({product.Stock} броя)!");
        }
        
        public void OnCategoryAdded(object sender, string category)
        {
            Console.WriteLine($"Уведомление: Нова категория '{category}' е добавена!");
        }
    }
}
