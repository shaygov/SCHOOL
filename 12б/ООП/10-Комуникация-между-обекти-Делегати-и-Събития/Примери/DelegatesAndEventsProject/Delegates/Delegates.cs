using System;

namespace DelegatesAndEventsExample.Delegates
{
    public delegate int MathOperation(int x, int y);
    public delegate void DisplayMessage(string message);
    public delegate bool ValidationRule(string input);
}
