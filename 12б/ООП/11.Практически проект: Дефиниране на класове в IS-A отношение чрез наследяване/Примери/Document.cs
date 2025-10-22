using System;

public class Document {
    protected string id;
    protected string title;
    protected int year;
    protected bool isAvailable;
    protected string location;
    protected static int documentCounter = 0;
    
    public Document() {
        this.id = GenerateId();
        this.title = "Неизвестен документ";
        this.year = DateTime.Now.Year;
        this.isAvailable = true;
        this.location = "Неопределено";
        documentCounter++;
    }
    
    public Document(string title, int year, string location) {
        this.id = GenerateId();
        this.title = title;
        this.year = year;
        this.isAvailable = true;
        this.location = location;
        documentCounter++;
    }
    
    public string Id {
        get { return id; }
    }
    
    public string Title {
        get { return title; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                title = value;
            }
        }
    }
    
    public int Year {
        get { return year; }
        set { 
            if (value > 0 && value <= DateTime.Now.Year) {
                year = value;
            }
        }
    }
    
    public bool IsAvailable {
        get { return isAvailable; }
    }
    
    public string Location {
        get { return location; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                location = value;
            }
        }
    }
    
    public static int TotalDocuments {
        get { return documentCounter; }
    }
    
    public virtual void Borrow() {
        if (isAvailable) {
            isAvailable = false;
            Console.WriteLine($"Документ '{title}' е зает");
        } else {
            Console.WriteLine($"Документ '{title}' не е наличен");
        }
    }
    
    public virtual void Return() {
        if (!isAvailable) {
            isAvailable = true;
            Console.WriteLine($"Документ '{title}' е върнат");
        } else {
            Console.WriteLine($"Документ '{title}' вече е наличен");
        }
    }
    
    public virtual string GetDocumentType() {
        return "Документ";
    }
    
    public virtual void DisplayInfo() {
        Console.WriteLine($"ID: {id}");
        Console.WriteLine($"Заглавие: {title}");
        Console.WriteLine($"Година: {year}");
        Console.WriteLine($"Тип: {GetDocumentType()}");
        Console.WriteLine($"Статус: {(isAvailable ? "Наличен" : "Зает")}");
        Console.WriteLine($"Местоположение: {location}");
    }
    
    private string GenerateId() {
        return $"DOC{DateTime.Now:yyyyMMdd}{documentCounter:D4}";
    }
    
    public static void DisplayLibraryStats() {
        Console.WriteLine($"Общо документи в библиотеката: {documentCounter}");
    }
}
