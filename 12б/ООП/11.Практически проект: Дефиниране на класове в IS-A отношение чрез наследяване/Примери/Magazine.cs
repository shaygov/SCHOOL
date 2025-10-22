using System;

public class Magazine : Document {
    private int issueNumber;
    private DateTime publicationDate;
    private string publisher;
    private string category;
    private bool isPeriodical;
    
    public Magazine() : base() {
        this.issueNumber = 0;
        this.publicationDate = DateTime.Now;
        this.publisher = "Неизвестен издател";
        this.category = "Общо";
        this.isPeriodical = true;
    }
    
    public Magazine(string title, int year, string location, int issueNumber,
                    DateTime publicationDate, string publisher, string category) 
                    : base(title, year, location) {
        this.issueNumber = issueNumber;
        this.publicationDate = publicationDate;
        this.publisher = publisher;
        this.category = category;
        this.isPeriodical = true;
    }
    
    public int IssueNumber {
        get { return issueNumber; }
        set { 
            if (value > 0) {
                issueNumber = value;
            }
        }
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
    
    public string Category {
        get { return category; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                category = value;
            }
        }
    }
    
    public bool IsPeriodical {
        get { return isPeriodical; }
    }
    
    public override string GetDocumentType() {
        return "Списание";
    }
    
    public override void DisplayInfo() {
        base.DisplayInfo();
        Console.WriteLine($"Номер: {issueNumber}");
        Console.WriteLine($"Дата на издаване: {publicationDate:dd.MM.yyyy}");
        Console.WriteLine($"Издател: {publisher}");
        Console.WriteLine($"Категория: {category}");
        Console.WriteLine($"Периодичност: {(isPeriodical ? "Да" : "Не")}");
    }
    
    public void Browse() {
        Console.WriteLine($"Разглеждане на списание '{title}' - брой {issueNumber}");
    }
    
    public bool IsRecent() {
        return (DateTime.Now - publicationDate).Days <= 30;
    }
    
    public string GetAge() {
        int days = (DateTime.Now - publicationDate).Days;
        if (days <= 7) return "Много ново";
        if (days <= 30) return "Ново";
        if (days <= 90) return "Сравнително ново";
        return "Старо";
    }
}
