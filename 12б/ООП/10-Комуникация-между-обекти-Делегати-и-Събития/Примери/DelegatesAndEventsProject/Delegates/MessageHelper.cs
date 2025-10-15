using System;

namespace DelegatesAndEventsExample.Delegates
{
    public static class MessageHelper
    {
        public static void PrintToConsole(string message)
        {
            Console.WriteLine($"Конзола: {message}");
        }
        
        public static void PrintToFile(string message)
        {
            Console.WriteLine($"Файл: {message}");
        }
        
        public static void PrintToDatabase(string message)
        {
            Console.WriteLine($"База данни: {message}");
        }
        
        public static void PrintWithTimestamp(string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}
