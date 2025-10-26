using System;
using System.Collections.Generic;
using System.Linq;

public class LibraryManager {
    private List<Document> documents;
    private Dictionary<string, List<Document>> borrowedItems;
    
    public LibraryManager() {
        documents = new List<Document>();
        borrowedItems = new Dictionary<string, List<Document>>();
    }
    
    public void AddDocument(Document document) {
        documents.Add(document);
        Console.WriteLine($"Добавен документ: {document.Title}");
    }
    
    public List<Document> SearchByTitle(string title) {
        return documents.Where(d => d.Title.ToLower().Contains(title.ToLower())).ToList();
    }
    
    public List<Document> SearchByType(string documentType) {
        return documents.Where(d => d.GetDocumentType() == documentType).ToList();
    }
    
    public void DisplayAllDocuments() {
        Console.WriteLine("=== ВСИЧКИ ДОКУМЕНТИ В БИБЛИОТЕКАТА ===");
        foreach (var doc in documents) {
            doc.DisplayInfo();
            Console.WriteLine(new string('-', 50));
        }
    }
    
    public void DisplayAvailableDocuments() {
        Console.WriteLine("=== НАЛИЧНИ ДОКУМЕНТИ ===");
        var available = documents.Where(d => d.IsAvailable).ToList();
        foreach (var doc in available) {
            doc.DisplayInfo();
            Console.WriteLine(new string('-', 30));
        }
    }
    
    public bool BorrowDocument(string documentId, string borrowerName) {
        var document = documents.FirstOrDefault(d => d.Id == documentId);
        if (document != null && document.IsAvailable) {
            document.Borrow();
            
            if (!borrowedItems.ContainsKey(borrowerName)) {
                borrowedItems[borrowerName] = new List<Document>();
            }
            borrowedItems[borrowerName].Add(document);
            
            Console.WriteLine($"Документ '{document.Title}' е зает от {borrowerName}");
            return true;
        }
        Console.WriteLine($"Документ с ID {documentId} не е наличен");
        return false;
    }
    
    public bool ReturnDocument(string documentId, string borrowerName) {
        var document = documents.FirstOrDefault(d => d.Id == documentId);
        if (document != null && !document.IsAvailable) {
            document.Return();
            
            if (borrowedItems.ContainsKey(borrowerName)) {
                borrowedItems[borrowerName].Remove(document);
            }
            
            Console.WriteLine($"Документ '{document.Title}' е върнат от {borrowerName}");
            return true;
        }
        Console.WriteLine($"Документ с ID {documentId} не може да бъде върнат");
        return false;
    }
    
    public void DisplayBorrowedByUser(string borrowerName) {
        if (borrowedItems.ContainsKey(borrowerName) && borrowedItems[borrowerName].Count > 0) {
            Console.WriteLine($"=== ЗАЕТИ ДОКУМЕНТИ ОТ {borrowerName.ToUpper()} ===");
            foreach (var doc in borrowedItems[borrowerName]) {
                doc.DisplayInfo();
                Console.WriteLine(new string('-', 30));
            }
        } else {
            Console.WriteLine($"{borrowerName} няма заети документи");
        }
    }
    
    public void DisplayLibraryStatistics() {
        Console.WriteLine("=== СТАТИСТИКА НА БИБЛИОТЕКАТА ===");
        Console.WriteLine($"Общо документи: {documents.Count}");
        Console.WriteLine($"Налични документи: {documents.Count(d => d.IsAvailable)}");
        Console.WriteLine($"Заети документи: {documents.Count(d => !d.IsAvailable)}");
        
        var typeStats = documents.GroupBy(d => d.GetDocumentType())
                               .Select(g => new { Type = g.Key, Count = g.Count() });
        
        Console.WriteLine("\nПо типове документи:");
        foreach (var stat in typeStats) {
            Console.WriteLine($"  {stat.Type}: {stat.Count}");
        }
    }
}
