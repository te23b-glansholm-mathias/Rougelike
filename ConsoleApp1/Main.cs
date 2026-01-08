using System.Diagnostics;

class Core
{
    public static string? languageKey = "En";
    public static string? playerName;
    public static Character? Player;
    public static Enemy? ActiveEnemy;

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

        Player = CreateNewPlayer();

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

    static Character CreateNewPlayer()
    {
        return new Character
        {
            Level = 1,
            HP = 100,
            MaxHP = 120,
            ATK = 25,
            FP = 0,
            MaxFP = 0
        };
    }

    static void ShowMenu()
    {
        bool show = true;

        while (show)
        {
            TextHandler.WriteText("intro_0", playerName!);

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
        WriteReadAndClear("battleBase_2", TextHandler.GetBlockText("tutorial_0"), TextHandler.GetBlockText("tutorial_0r2"));
        WriteReadAndClear("battleBase_3", TextHandler.GetBlockText("tutorial_1"), TextHandler.GetBlockText("tutorial_1r2"), TextHandler.GetBlockText("tutorial_1r3"));
        WriteReadAndClear("battleBase_1", TextHandler.GetBlockText("tutorial_2"));
        WriteReadAndClear("battleBase_1", TextHandler.GetBlockText("tutorial_3"));

        TutorialBattle();
    }

    static void WriteReadAndClear(params string[] texts)
    {
        Console.Clear();
        TextHandler.WriteText(texts[0], texts[1..]);
        Console.ReadLine();
    }

    static void TutorialBattle()
    {
        Common Frog = new()
        {
            name = "Frog",
            HP = 80,
            ATK = 6,
            LVL = 1
        };

        ActiveEnemy = Frog;

        WriteReadAndClear("enemyBattleBase_1", TextHandler.GetBlockText("fightStart"));
    }
}