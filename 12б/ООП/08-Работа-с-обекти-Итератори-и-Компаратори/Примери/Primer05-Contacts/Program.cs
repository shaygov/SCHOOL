using System;
using System.Collections;
using System.Collections.Generic;

class Contact : IComparable<Contact>
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool IsFavorite { get; set; }
    
    public Contact(string name, string phone, string email = "")
    {
        Name = name;
        Phone = phone;
        Email = email;
        IsFavorite = false;
    }
    
    public int CompareTo(Contact other)
    {
        if (other == null) return 1;
        return Name.CompareTo(other.Name);
    }
    
    public override string ToString()
    {
        string fav = IsFavorite ? "★" : " ";
        string emailPart = string.IsNullOrEmpty(Email) ? "" : $" | {Email}";
        return $"[{fav}] {Name,-20} | {Phone}{emailPart}";
    }
}

class PhoneBook : IEnumerable<Contact>
{
    private List<Contact> contacts;
    
    public PhoneBook()
    {
        contacts = new List<Contact>();
    }
    
    public void AddContact(Contact contact)
    {
        contacts.Add(contact);
    }
    
    public void ToggleFavorite(string name)
    {
        foreach (var contact in contacts)
        {
            if (contact.Name == name)
            {
                contact.IsFavorite = !contact.IsFavorite;
                break;
            }
        }
    }
    
    public IEnumerator<Contact> GetEnumerator()
    {
        foreach (var contact in contacts)
        {
            yield return contact;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public IEnumerable<Contact> GetFavorites()
    {
        foreach (var contact in contacts)
        {
            if (contact.IsFavorite)
            {
                yield return contact;
            }
        }
    }
    
    public IEnumerable<Contact> SearchByName(string searchTerm)
    {
        foreach (var contact in contacts)
        {
            if (contact.Name.ToLower().Contains(searchTerm.ToLower()))
            {
                yield return contact;
            }
        }
    }
    
    public void Sort(IComparer<Contact> comparer = null)
    {
        if (comparer != null)
        {
            contacts.Sort(comparer);
        }
        else
        {
            contacts.Sort();
        }
    }
}

class ContactPhoneComparer : IComparer<Contact>
{
    public int Compare(Contact x, Contact y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        
        return x.Phone.CompareTo(y.Phone);
    }
}

class Program
{
    static void Main()
    {
        var phoneBook = new PhoneBook();
        
        phoneBook.AddContact(new Contact("Иван Петров", "0888123456", "ivan@email.com"));
        phoneBook.AddContact(new Contact("Мария Георгиева", "0877234567", "maria@email.com"));
        phoneBook.AddContact(new Contact("Петър Стоянов", "0899345678"));
        phoneBook.AddContact(new Contact("Анна Димитрова", "0887456789", "anna@email.com"));
        phoneBook.AddContact(new Contact("Георги Иванов", "0898567890"));
        
        Console.WriteLine("=== ТЕЛЕФОНЕН УКАЗАТЕЛ ===\n");
        
        Console.WriteLine("Всички контакти:");
        foreach (var contact in phoneBook)
        {
            Console.WriteLine(contact);
        }
        
        phoneBook.ToggleFavorite("Мария Георгиева");
        phoneBook.ToggleFavorite("Петър Стоянов");
        
        Console.WriteLine("\n=== Любими контакти ===");
        foreach (var contact in phoneBook.GetFavorites())
        {
            Console.WriteLine(contact);
        }
        
        Console.WriteLine("\n=== Търсене по име 'Петр' ===");
        foreach (var contact in phoneBook.SearchByName("Петр"))
        {
            Console.WriteLine(contact);
        }
        
        Console.WriteLine("\n=== Сортирани по име ===");
        phoneBook.Sort();
        foreach (var contact in phoneBook)
        {
            Console.WriteLine(contact);
        }
        
        Console.WriteLine("\n=== Сортирани по телефон ===");
        phoneBook.Sort(new ContactPhoneComparer());
        foreach (var contact in phoneBook)
        {
            Console.WriteLine(contact);
        }
    }
}

