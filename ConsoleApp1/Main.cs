using System.Diagnostics;

class Core
{
    public static string? languageKey = "En";
    public static string? playerName;

    [STAThread]
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

        Console.ReadLine();
    }

    static void Start()
    {
        ChooseLanguage();
        Console.Clear();

        Console.WriteLine("What is your name?");
        playerName = Console.ReadLine();
        Console.Clear();

        ShowMenu();
    }

    static void ChooseLanguage()
    {
        Console.WriteLine("Choose language");

        while (true)
        {
            Console.WriteLine(
            """
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

                default:
                    Console.Clear();
                    Console.WriteLine("Please choose a valid language");
                    continue;
            }

            TextHandler.ReadFile(languageKey);
            break;
        }
    }

    static void ShowMenu()
    {
        bool show = true;

        while (show)
        {
            TextHandler.WriteText("intro_0", playerName);

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.A:
                    show = false;
                    TutorialRequest();
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

            if (show) Console.Clear();
        }
    }

    static void TutorialRequest()
    {
        Console.Clear();
        Console.WriteLine("Do you want to play the tutorial? [Y/N] ");

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Y:
                    StartTutorial();
                    break;

                case ConsoleKey.N:
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Please choose a valid input if you wanna play the tutorial [Y/N]. The Y or N key");
                    continue;
            }
            break;
        }

    }

    static void NewWindow(string args)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c start \"\" \"{Environment.ProcessPath}\" {args}",
        });
    }

    static void StartTutorial()
    {
        Console.Clear();
        TextHandler.WriteText("battleBase_0", playerName, TextHandler.GetBlockText("test"));
        Console.ReadLine();



        Console.Clear();
        TextHandler.WriteText("tutorial_0", playerName);
        Console.ReadLine();
        Console.Clear();
        TextHandler.WriteText("resize", playerName);
        Console.ReadLine();
        Console.Clear();
        TextHandler.WriteText("tutorial_1", playerName);
        Console.ReadLine();
        Console.Clear();
        TextHandler.WriteText("tutorial_2", playerName);
        Console.ReadLine();


    }

    static void TutorialBattle()
    {
        Enemy Frog = new()
        {
            name = "Frog",
            hp = 80,
            ATK = 6
        };


    }
}