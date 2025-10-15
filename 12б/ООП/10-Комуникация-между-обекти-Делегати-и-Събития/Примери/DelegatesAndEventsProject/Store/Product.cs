using System;

namespace DelegatesAndEventsExample.Store
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
        
        public Product(string name, decimal price, int stock, string category)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Category = category;
        }
        
        public override string ToString()
        {
            return $"{Name} - {Price:C} (Наличност: {Stock})";
        }
    }
}
