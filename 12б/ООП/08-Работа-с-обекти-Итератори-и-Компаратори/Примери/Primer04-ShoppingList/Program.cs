using System;
using System.Collections;
using System.Collections.Generic;

class Product : IComparable<Product>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsBought { get; set; }
    
    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        IsBought = false;
    }
    
    public decimal TotalPrice => Price * Quantity;
    
    public int CompareTo(Product other)
    {
        if (other == null) return 1;
        return Name.CompareTo(other.Name);
    }
    
    public override string ToString()
    {
        string status = IsBought ? "✓" : "○";
        return $"[{status}] {Name,-20} x{Quantity}бр = {TotalPrice:F2}лв";
    }
}

class ShoppingList : IEnumerable<Product>
{
    private List<Product> products;
    
    public ShoppingList()
    {
        products = new List<Product>();
    }
    
    public void AddProduct(Product product)
    {
        products.Add(product);
    }
    
    public void MarkAsBought(string name)
    {
        foreach (var product in products)
        {
            if (product.Name == name)
            {
                product.IsBought = true;
                break;
            }
        }
    }
    
    public IEnumerator<Product> GetEnumerator()
    {
        foreach (var product in products)
        {
            yield return product;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public IEnumerable<Product> GetBoughtProducts()
    {
        foreach (var product in products)
        {
            if (product.IsBought)
            {
                yield return product;
            }
        }
    }
    
    public IEnumerable<Product> GetNotBoughtProducts()
    {
        foreach (var product in products)
        {
            if (!product.IsBought)
            {
                yield return product;
            }
        }
    }
    
    public decimal GetTotalPrice()
    {
        decimal total = 0;
        foreach (var product in products)
        {
            total += product.TotalPrice;
        }
        return total;
    }
    
    public void Sort(IComparer<Product> comparer = null)
    {
        if (comparer != null)
        {
            products.Sort(comparer);
        }
        else
        {
            products.Sort();
        }
    }
}

class ProductPriceComparer : IComparer<Product>
{
    public int Compare(Product x, Product y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        return y.TotalPrice.CompareTo(x.TotalPrice);
    }
}

class Program
{
    static void Main()
    {
        var list = new ShoppingList();
        
        list.AddProduct(new Product("Хляб", 1.50m, 2));
        list.AddProduct(new Product("Мляко", 2.80m, 3));
        list.AddProduct(new Product("Яйца", 4.50m, 1));
        list.AddProduct(new Product("Сирене", 8.00m, 1));
        list.AddProduct(new Product("Домати", 3.20m, 2));
        
        Console.WriteLine("=== СПИСЪК ЗА ПАЗАРУВАНЕ ===\n");
        
        Console.WriteLine("Всички продукти:");
        foreach (var product in list)
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine($"\nОбща сума: {list.GetTotalPrice():F2}лв");
        
        list.MarkAsBought("Хляб");
        list.MarkAsBought("Мляко");
        
        Console.WriteLine("\n=== След като купих хляб и мляко ===\n");
        
        Console.WriteLine("Купени:");
        foreach (var product in list.GetBoughtProducts())
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\nОстава да купя:");
        foreach (var product in list.GetNotBoughtProducts())
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== Сортирани по цена (най-скъпи първи) ===");
        list.Sort(new ProductPriceComparer());
        foreach (var product in list)
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== Сортирани по име ===");
        list.Sort();
        foreach (var product in list)
        {
            Console.WriteLine(product);
        }
    }
}

