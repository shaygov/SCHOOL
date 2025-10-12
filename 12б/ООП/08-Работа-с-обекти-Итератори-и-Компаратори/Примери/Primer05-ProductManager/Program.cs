using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class Product : IComparable<Product>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; }
    public string Manufacturer { get; set; }
    public DateTime AddedDate { get; set; }
    
    public Product(int id, string name, decimal price, int quantity, string category, string manufacturer)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        Category = category;
        Manufacturer = manufacturer;
        AddedDate = DateTime.Now;
    }
    
    public decimal TotalValue => Price * Quantity;
    public bool InStock => Quantity > 0;
    
    public int CompareTo(Product other)
    {
        if (other == null) return 1;
        
        int categoryComparison = string.Compare(Category, other.Category, StringComparison.OrdinalIgnoreCase);
        if (categoryComparison != 0) return categoryComparison;
        
        return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }
    
    public override string ToString()
    {
        return $"[{Id}] {Name} - {Price:F2}лв x {Quantity} = {TotalValue:F2}лв ({Category})";
    }
}

class ProductManager : IEnumerable<Product>
{
    private List<Product> products;
    
    public ProductManager()
    {
        products = new List<Product>();
    }
    
    public void AddProduct(Product product)
    {
        products.Add(product);
    }
    
    public void RemoveProduct(int id)
    {
        products.RemoveAll(p => p.Id == id);
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
    
    public IEnumerable<Product> GetInStockProducts()
    {
        foreach (var product in products)
        {
            if (product.InStock)
            {
                yield return product;
            }
        }
    }
    
    public IEnumerable<Product> GetOutOfStockProducts()
    {
        foreach (var product in products)
        {
            if (!product.InStock)
            {
                yield return product;
            }
        }
    }
    
    public IEnumerable<Product> GetProductsByCategory(string category)
    {
        foreach (var product in products)
        {
            if (product.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            {
                yield return product;
            }
        }
    }
    
    public IEnumerable<Product> GetProductsByManufacturer(string manufacturer)
    {
        foreach (var product in products)
        {
            if (product.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase))
            {
                yield return product;
            }
        }
    }
    
    public IEnumerable<Product> GetProductsInPriceRange(decimal minPrice, decimal maxPrice)
    {
        foreach (var product in products)
        {
            if (product.Price >= minPrice && product.Price <= maxPrice)
            {
                yield return product;
            }
        }
    }
    
    public IEnumerable<Product> GetLowStockProducts(int threshold)
    {
        foreach (var product in products)
        {
            if (product.Quantity > 0 && product.Quantity <= threshold)
            {
                yield return product;
            }
        }
    }
    
    public IEnumerable<Product> SearchByName(string searchTerm)
    {
        foreach (var product in products)
        {
            if (product.Name.ToLower().Contains(searchTerm.ToLower()))
            {
                yield return product;
            }
        }
    }
    
    public void SortProducts(IComparer<Product> comparer = null)
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
    
    public void DisplayStatistics()
    {
        Console.WriteLine($"Общо продукти: {products.Count}");
        Console.WriteLine($"В наличност: {GetInStockProducts().Count()}");
        Console.WriteLine($"Изчерпани: {GetOutOfStockProducts().Count()}");
        Console.WriteLine($"Обща стойност: {products.Sum(p => p.TotalValue):F2}лв");
        Console.WriteLine($"Средна цена: {products.Average(p => p.Price):F2}лв");
        
        var categories = products.GroupBy(p => p.Category);
        Console.WriteLine("\nПродукти по категория:");
        foreach (var category in categories)
        {
            Console.WriteLine($"  {category.Key}: {category.Count()} продукта");
        }
    }
}

class ProductNameComparer : IComparer<Product>
{
    public int Compare(Product x, Product y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
    }
}

class ProductPriceComparer : IComparer<Product>
{
    private bool ascending;
    
    public ProductPriceComparer(bool ascending = true)
    {
        this.ascending = ascending;
    }
    
    public int Compare(Product x, Product y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        int result = x.Price.CompareTo(y.Price);
        return ascending ? result : -result;
    }
}

class ProductQuantityComparer : IComparer<Product>
{
    public int Compare(Product x, Product y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        return y.Quantity.CompareTo(x.Quantity);
    }
}

class ProductTotalValueComparer : IComparer<Product>
{
    public int Compare(Product x, Product y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        return y.TotalValue.CompareTo(x.TotalValue);
    }
}

class Program
{
    static void Main()
    {
        var manager = new ProductManager();
        
        manager.AddProduct(new Product(1, "Лаптоп Dell", 1200.00m, 5, "Електроника", "Dell"));
        manager.AddProduct(new Product(2, "Мишка Logitech", 25.00m, 50, "Периферия", "Logitech"));
        manager.AddProduct(new Product(3, "Клавиатура Razer", 89.00m, 30, "Периферия", "Razer"));
        manager.AddProduct(new Product(4, "Монитор Samsung", 450.00m, 8, "Електроника", "Samsung"));
        manager.AddProduct(new Product(5, "USB Кабел", 5.00m, 100, "Аксесоари", "Generic"));
        manager.AddProduct(new Product(6, "Слушалки Sony", 120.00m, 0, "Аудио", "Sony"));
        manager.AddProduct(new Product(7, "Уеб камера Logitech", 75.00m, 12, "Периферия", "Logitech"));
        manager.AddProduct(new Product(8, "Принтер HP", 280.00m, 3, "Електроника", "HP"));
        manager.AddProduct(new Product(9, "Хард диск Seagate", 95.00m, 15, "Компоненти", "Seagate"));
        manager.AddProduct(new Product(10, "SSD Samsung", 150.00m, 20, "Компоненти", "Samsung"));
        
        Console.WriteLine("=== СИСТЕМА ЗА УПРАВЛЕНИЕ НА ПРОДУКТИ ===\n");
        manager.DisplayStatistics();
        
        Console.WriteLine("\n=== ВСИЧКИ ПРОДУКТИ ===");
        foreach (var product in manager)
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== ПРОДУКТИ В НАЛИЧНОСТ ===");
        foreach (var product in manager.GetInStockProducts())
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== ИЗЧЕРПАНИ ПРОДУКТИ ===");
        foreach (var product in manager.GetOutOfStockProducts())
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== ПРОДУКТИ С НИСЪК ЗАПАС (≤ 10) ===");
        foreach (var product in manager.GetLowStockProducts(10))
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== ПРОДУКТИ В КАТЕГОРИЯ 'Електроника' ===");
        foreach (var product in manager.GetProductsByCategory("Електроника"))
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== ПРОДУКТИ ОТ ПРОИЗВОДИТЕЛ 'Samsung' ===");
        foreach (var product in manager.GetProductsByManufacturer("Samsung"))
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== ПРОДУКТИ В ЦЕНОВИ ДИАПАЗОН 50-150 ЛВ ===");
        foreach (var product in manager.GetProductsInPriceRange(50, 150))
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== ТЪРСЕНЕ ПО ИМЕ 'Лаптоп' ===");
        foreach (var product in manager.SearchByName("Лаптоп"))
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== СОРТИРАНЕ ПО ИМЕ ===");
        manager.SortProducts(new ProductNameComparer());
        foreach (var product in manager)
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== СОРТИРАНЕ ПО ЦЕНА (ВЪЗХОДЯЩО) ===");
        manager.SortProducts(new ProductPriceComparer(true));
        foreach (var product in manager)
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== СОРТИРАНЕ ПО ЦЕНА (НИЗХОДЯЩО) ===");
        manager.SortProducts(new ProductPriceComparer(false));
        foreach (var product in manager)
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== СОРТИРАНЕ ПО КОЛИЧЕСТВО ===");
        manager.SortProducts(new ProductQuantityComparer());
        foreach (var product in manager)
        {
            Console.WriteLine(product);
        }
        
        Console.WriteLine("\n=== СОРТИРАНЕ ПО ОБЩА СТОЙНОСТ ===");
        manager.SortProducts(new ProductTotalValueComparer());
        foreach (var product in manager)
        {
            Console.WriteLine(product);
        }
    }
}

