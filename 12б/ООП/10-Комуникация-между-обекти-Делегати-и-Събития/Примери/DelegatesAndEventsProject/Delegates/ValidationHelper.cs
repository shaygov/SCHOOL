using System;

namespace DelegatesAndEventsExample.Delegates
{
    public static class ValidationHelper
    {
        public static bool IsNotEmpty(string input)
        {
            return !string.IsNullOrEmpty(input);
        }
        
        public static bool IsEmail(string input)
        {
            return input.Contains("@") && input.Contains(".");
        }
        
        public static bool IsLongEnough(string input)
        {
            return input.Length >= 5;
        }
        
        public static bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }
    }
}
