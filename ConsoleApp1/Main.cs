using System.Diagnostics;
using System.Reflection.Metadata;

class Core
{
    public static string? languageKey;

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        if (args.Length > 0)
        {
            switch (args[0])
            {
                case "achievements":
                    Achievements.Show();
                    break;
                case "settings":
                    Settings.Show();
                    break;
            }
        }
        else
        {
            Console.Title = "Main";
            Start();
        }
    } 

    static void Start()
    {
        string? name;
        bool startMenu = true;

        Console.WriteLine("Choose language");
        ChooseLanguage();
        Console.Clear();

        Console.WriteLine("What is your name");
        name = Console.ReadLine();
        Console.Clear();

        while (startMenu == true) ShowMenu();

        Console.ReadLine();
    }

    static void ChooseLanguage()
    {
        Console.WriteLine(
        """
        Choose language
        a - English
        b - 日本語
        c - Brainrot
        """);

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        switch (keyInfo.Key)
        {
            case ConsoleKey.A:
                languageKey = "En";
                break;

            case ConsoleKey.B:
                languageKey = "Jp";
                break;

            case ConsoleKey.C:
                languageKey = "Br";
                break;
        }
    }

    static void ShowMenu()
    {
        TextHandler.WriteText(1, languageKey);

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        switch (keyInfo.Key)
        {
            case ConsoleKey.A:

                break;

            case ConsoleKey.B:
                NewWindow("settings");
                break;

            case ConsoleKey.C:
                NewWindow("achievements");
                break;

            case ConsoleKey.D:
                Environment.Exit(0);
                break;
        }

        Console.Clear();    
    }

    static void NewWindow(string args)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c start \"\" \"{Environment.ProcessPath}\" {args}",
        });
    }
}