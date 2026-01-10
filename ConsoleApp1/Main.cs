using System.Diagnostics;

class Core
{
    public static string? playerName;
    public static Character? Player;
    public static Enemy? ActiveEnemy;
    public static string? ActiveAttack;
    public static float ActiveDamage;

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
        playerName = Console.ReadLine();
        Console.Clear();

        Player = new("Warrior");

        MenuManager.ShowMenu(playerName!);
    }

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