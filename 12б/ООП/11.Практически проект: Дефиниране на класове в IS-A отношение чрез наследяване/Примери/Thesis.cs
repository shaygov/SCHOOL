using System;

public class Thesis : Document {
    private string author;
    private string university;
    private string faculty;
    private string supervisor;
    private int defenseYear;
    private string degree;
    
    public Thesis() : base() {
        this.author = "Неизвестен автор";
        this.university = "Неизвестен университет";
        this.faculty = "Неизвестен факултет";
        this.supervisor = "Неизвестен ръководител";
        this.defenseYear = DateTime.Now.Year;
        this.degree = "Бакалавър";
    }
    
    public Thesis(string title, int year, string location, string author,
                  string university, string faculty, string supervisor, 
                  int defenseYear, string degree) 
                  : base(title, year, location) {
        this.author = author;
        this.university = university;
        this.faculty = faculty;
        this.supervisor = supervisor;
        this.defenseYear = defenseYear;
        this.degree = degree;
    }
    
    public string Author {
        get { return author; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                author = value;
            }
        }
    }
    
    public string University {
        get { return university; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                university = value;
            }
        }
    }
    
    public string Faculty {
        get { return faculty; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                faculty = value;
            }
        }
    }
    
    public string Supervisor {
        get { return supervisor; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                supervisor = value;
            }
        }
    }
    
    public int DefenseYear {
        get { return defenseYear; }
        set { 
            if (value > 0 && value <= DateTime.Now.Year) {
                defenseYear = value;
            }
        }
    }
    
    public string Degree {
        get { return degree; }
        set { 
            if (!string.IsNullOrEmpty(value)) {
                degree = value;
            }
        }
    }
    
    public override string GetDocumentType() {
        return "Дипломна работа";
    }
    
    public override void DisplayInfo() {
        base.DisplayInfo();
        Console.WriteLine($"Автор: {author}");
        Console.WriteLine($"Университет: {university}");
        Console.WriteLine($"Факултет: {faculty}");
        Console.WriteLine($"Ръководител: {supervisor}");
        Console.WriteLine($"Година на защита: {defenseYear}");
        Console.WriteLine($"Степен: {degree}");
    }
    
    public void Study() {
        Console.WriteLine($"Изучаване на дипломна работа '{title}' от {author}");
    }
    
    public bool IsRecentThesis() {
        return (DateTime.Now.Year - defenseYear) <= 5;
    }
    
    public string GetAcademicLevel() {
        switch (degree.ToLower()) {
            case "бакалавър": return "Основна степен";
            case "магистър": return "Магистърска степен";
            case "доктор": return "Докторска степен";
            default: return "Неопределена степен";
        }
    }
    
    public bool IsFromUniversity(string universityName) {
        return university.Equals(universityName, StringComparison.OrdinalIgnoreCase);
    }
}
