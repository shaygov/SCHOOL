using System;

class Book : IReadable, IPrintable
{
    private string title;
    private string author;
    private string content;
    private int pageCount;
    private bool isAvailable;

    public Book(string title, string author, string content, int pageCount)
    {
        this.title = title;
        this.author = author;
        this.content = content;
        this.pageCount = pageCount;
        this.isAvailable = true;
    }

    public void Read()
    {
        if (isAvailable)
        {
            Console.WriteLine($"Чета книгата '{title}' от {author}");
            Console.WriteLine($"Съдържание: {content}");
        }
        else
        {
            Console.WriteLine("Книгата не е налична за четене");
        }
    }

    public string GetContent()
    {
        return content;
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    public void Print()
    {
        Console.WriteLine($"Печатам книгата '{title}' - {pageCount} страници");
    }

    public int GetPageCount()
    {
        return pageCount;
    }

    public void Borrow()
    {
        if (isAvailable)
        {
            isAvailable = false;
            Console.WriteLine($"Книгата '{title}' е взета назаем");
        }
        else
        {
            Console.WriteLine("Книгата вече е взета");
        }
    }

    public void Return()
    {
        isAvailable = true;
        Console.WriteLine($"Книгата '{title}' е върната");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Заглавие: {title}");
        Console.WriteLine($"Автор: {author}");
        Console.WriteLine($"Страници: {pageCount}");
        Console.WriteLine($"Налична: {(isAvailable ? "Да" : "Не")}");
    }
}
