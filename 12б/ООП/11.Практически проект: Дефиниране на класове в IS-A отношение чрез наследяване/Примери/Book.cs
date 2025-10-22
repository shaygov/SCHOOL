using System;

public class Book : Document {
    private string author;
    private string isbn;
    private int pages;
    private string genre;
    
    public Book() : base() {
        this.author = "Неизвестен автор";
        this.isbn = "";
        this.pages = 0;
        this.genre = "Неопределен жанр";
    }
    
    public Book(string title, int year, string location, string author, 
                string isbn, int pages, string genre) 
                : base(title, year, location) {
        this.author = author;
        this.isbn = isbn;
        this.pages = pages;
        this.genre = genre;
    }
    
    public string Author {
        get { return author; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                author = value;
            }
        }
    }
    
    public string ISBN {
        get { return isbn; }
        set { isbn = value; }
    }
    
    public int Pages {
        get { return pages; }
        set { 
            if (value > 0) {
                pages = value;
            }
        }
    }
    
    public string Genre {
        get { return genre; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                genre = value;
            }
        }
    }
    
    public override string GetDocumentType() {
        return "Книга";
    }
    
    public override void DisplayInfo() {
        base.DisplayInfo();
        Console.WriteLine($"Автор: {author}");
        Console.WriteLine($"ISBN: {isbn}");
        Console.WriteLine($"Страници: {pages}");
        Console.WriteLine($"Жанр: {genre}");
    }
    
    public void Read() {
        Console.WriteLine($"Четене на книга '{title}' от {author}");
    }
    
    public bool IsLongBook() {
        return pages > 500;
    }
    
    public string GetReadingTime() {
        if (pages <= 100) return "1-2 часа";
        if (pages <= 300) return "3-5 часа";
        if (pages <= 500) return "6-8 часа";
        return "Повече от 8 часа";
    }
}
