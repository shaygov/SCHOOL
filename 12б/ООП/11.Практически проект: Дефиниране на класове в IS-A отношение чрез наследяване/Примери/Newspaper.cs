using System;

public class Newspaper : Document {
    private DateTime publicationDate;
    private string publisher;
    private int circulation;
    private string language;
    
    public Newspaper() : base() {
        this.publicationDate = DateTime.Now;
        this.publisher = "Неизвестен издател";
        this.circulation = 0;
        this.language = "Български";
    }
    
    public Newspaper(string title, int year, string location, DateTime publicationDate,
                     string publisher, int circulation, string language) 
                     : base(title, year, location) {
        this.publicationDate = publicationDate;
        this.publisher = publisher;
        this.circulation = circulation;
        this.language = language;
    }
    
    public DateTime PublicationDate {
        get { return publicationDate; }
        set { publicationDate = value; }
    }
    
    public string Publisher {
        get { return publisher; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                publisher = value;
            }
        }
    }
    
    public int Circulation {
        get { return circulation; }
        set { 
            if (value >= 0) {
                circulation = value;
            }
        }
    }
    
    public string Language {
        get { return language; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                language = value;
            }
        }
    }
    
    public override string GetDocumentType() {
        return "Вестник";
    }
    
    public override void DisplayInfo() {
        base.DisplayInfo();
        Console.WriteLine($"Дата на издаване: {publicationDate:dd.MM.yyyy}");
        Console.WriteLine($"Издател: {publisher}");
        Console.WriteLine($"Тираж: {circulation:N0}");
        Console.WriteLine($"Език: {language}");
    }
    
    public void ReadNews() {
        Console.WriteLine($"Четене на новини от '{title}' за {publicationDate:dd.MM.yyyy}");
    }
    
    public bool IsToday() {
        return publicationDate.Date == DateTime.Now.Date;
    }
    
    public bool IsPopular() {
        return circulation > 100000;
    }
    
    public string GetNewsAge() {
        int hours = (int)(DateTime.Now - publicationDate).TotalHours;
        if (hours <= 1) return "Много свежи новини";
        if (hours <= 6) return "Свежи новини";
        if (hours <= 24) return "Вчерашни новини";
        return "Стари новини";
    }
}
