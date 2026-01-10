using System.Diagnostics;

class GameHandler
{
    public static Character? Player { get; private set; }
    public static Enemy? ActiveEnemy { get; private set; }
    public static string? PlayerName { get; private set; }
    public static string? ActiveAttack { get; private set; }
    public static float ActiveDamage { get; private set; }

    public static void SetPlayer(Character who) => Player = who;
    public static void SetActive(Enemy who) => ActiveEnemy = who;
    public static void SetName(string name) => PlayerName = name;
    public static void SetAttack(string attack, float damage)
    {
        ActiveAttack = attack;
        ActiveDamage = damage;
    }
}

class Core
{
    static void Main(string[] argument)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        if (argument.Length > 0)
        {
            switch (argument[0])
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
        GameHandler.SetName(Console.ReadLine()!);
        Console.Clear();
        GameHandler.SetPlayer(new("Warrior"));
        MenuManager.ShowMenu();
    }
}

class ConsoleManager()
{
    static public void NewWindow(string argument)
    {
        ProcessStartInfo info = new()
        {
            FileName = "cmd.exe",
            Arguments = $""" /c start "" "{Environment.ProcessPath}" {argument} """,
        };

        Process.Start(info);
    }

    static public void WriteReadAndClear(params string[] texts)
    {
        Console.Clear();
        TextHandler.WriteText(texts[0], texts[1..]);
        Console.ReadLine();
    }
}