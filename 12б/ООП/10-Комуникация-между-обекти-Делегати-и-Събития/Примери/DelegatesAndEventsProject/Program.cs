using System;
using DelegatesAndEventsExample.Delegates;
using DelegatesAndEventsExample.Events;
using DelegatesAndEventsExample.BuiltInDelegates;
using DelegatesAndEventsExample.Store;

namespace DelegatesAndEventsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                       12б клас, ООП                         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        DelegatesDemo.RunDemo();
                        WaitForKey();
                        break;
                    case "2":
                        Console.Clear();
                        EventsDemo.RunDemo();
                        WaitForKey();
                        break;
                    case "3":
                        Console.Clear();
                        BuiltInDelegatesDemo.RunDemo();
                        WaitForKey();
                        break;
                    case "4":
                        Console.Clear();
                        RunCompleteExample();
                        WaitForKey();
                        break;
                    case "0":
                        Console.WriteLine("Благодаря за вниманието!");
                        return;
                    default:
                        Console.WriteLine("Невалиден избор. Моля, опитайте отново.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n┌─────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                        ГЛАВНО МЕНЮ                        │");
            Console.WriteLine("├─────────────────────────────────────────────────────────────┤");
            Console.WriteLine("│ 1. Демонстрация на делегати                                │");
            Console.WriteLine("│ 2. Демонстрация на събития                                 │");
            Console.WriteLine("│ 3. Вградени делегати (Action, Func, Predicate, Comparison) │");
            Console.WriteLine("│ 4. Пълен пример - Система за управление на магазин         │");
            Console.WriteLine("│ 0. Изход                                                    │");
            Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
            Console.Write("Изберете опция (0-4): ");
        }

        static void WaitForKey()
        {
            Console.WriteLine("\nНатиснете Enter за да продължите...");
            Console.ReadLine();
            Console.Clear();
        }

        static void RunCompleteExample()
        {
            Console.WriteLine("=== ПЪЛЕН ПРИМЕР - СИСТЕМА ЗА УПРАВЛЕНИЕ НА МАГАЗИН ===\n");

            var store = new Store.Store();
            var statistics = new StatisticsManager();
            var inventory = new InventoryManager();
            var reports = new ReportGenerator();
            var notifications = new StoreNotificationService();

            store.ProductAdded += statistics.OnProductAdded;
            store.ProductAdded += reports.OnProductAdded;
            store.ProductAdded += notifications.OnProductAdded;

            store.ProductSold += statistics.OnProductSold;
            store.ProductSold += reports.OnProductSold;
            store.ProductSold += inventory.OnProductSold;
            store.ProductSold += notifications.OnProductSold;

            store.LowStockWarning += inventory.OnLowStockWarning;
            store.LowStockWarning += notifications.OnLowStockWarning;

            store.CategoryAdded += statistics.OnCategoryAdded;
            store.CategoryAdded += reports.OnCategoryAdded;
            store.CategoryAdded += notifications.OnCategoryAdded;

            Console.WriteLine("Добавяне на продукти:");
            store.AddProduct(new Product("Laptop", 1500, 10, "Електроника"));
            store.AddProduct(new Product("Книга", 25, 50, "Книги"));
            store.AddProduct(new Product("Молив", 2, 100, "Канцеларски"));
            store.AddProduct(new Product("Телефон", 800, 5, "Електроника"));
            Console.WriteLine();

            Console.WriteLine("Продажби:");
            store.SellProduct("Laptop", 2);
            store.SellProduct("Книга", 10);
            store.SellProduct("Молив", 50);
            store.SellProduct("Телефон", 4);
            Console.WriteLine();

            statistics.DisplayStatistics();
            inventory.DisplayLowStockProducts();
            reports.GenerateReport();
        }
    }
}
