using System.Diagnostics;

class Core
{
    public static string? playerName;
    public static Character? Player;
    public static Enemy? ActiveEnemy;
    public static string? ActiveAttack;

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
        LanguageManager.ChooseLanguage();
        Console.Clear();

        Console.WriteLine("What is your name?");
        playerName = Console.ReadLine();
        Console.Clear();

        Player = CreateNewPlayer();

        MenuManager.ShowMenu(playerName!);
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

    static public void NewWindow(string args)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c start \"\" \"{Environment.ProcessPath}\" {args}",
        });
    }

    static public void WriteReadAndClear(params string[] texts)
    {
        Console.Clear();
        TextHandler.WriteText(texts[0], texts[1..]);
        Console.ReadLine();
    }
}