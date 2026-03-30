using SmartHomeControl;

var app = new SmartHomeApp();
app.ShowWelcome();

var isRunning = true;

while (isRunning)
{
    app.PrintMenu();
    var input = Console.ReadLine() ?? string.Empty;
    isRunning = app.HandleMenuSelection(input.Trim());
}

Console.WriteLine("Goodbye.");
