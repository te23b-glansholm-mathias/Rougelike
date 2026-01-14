using System.Diagnostics; // used for processes

// Handles current player info
static class GameHandler
{
    // should not be changed outside "GameHandler"
    public static Character? Player { get; private set; }
    // public static Enemy? ActiveEnemy { get; private set; }
    public static List<Enemy>? ActiveEnemies { get; private set; } = new();
    public static string? PlayerName { get; private set; }
    public static List<string>? ActiveAttack { get; private set; } = new();
    public static List<float> ActiveDamage { get; private set; } = new();

    // sets variable depending on parameter
    public static void SetPlayer(Character who) => Player = who;

    public static void SetActive(Enemy enemy) => ActiveEnemies?.Add(enemy);
    public static void SetName(string name) => PlayerName = name;
    public static void SetAttack(string attack, float damage = 0)
    {
        ActiveAttack!.Add(attack);
        ActiveDamage!.Add(damage);
    }

    public static void ResetAttack()
    {
        ActiveAttack!.Clear();
        ActiveDamage!.Clear();
    }
}

static class Core
{
    // Runs first (necessary signature)
    static void Main(string[] argument)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        if (argument.Length > 0) // if called with argument
        {
            switch (argument[0])    // check the argument content
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

    // starts the games
    static void Start()
    {
        LanguageManager.ChooseLanguage();
        Console.Clear();
        Console.WriteLine("What is your name?");
        GameHandler.SetName(Console.ReadLine()!);
        Console.Clear();
        GameHandler.SetPlayer(new("Warrior")); // creates a new character with the warrior class
        MenuManager.ShowMenu();
    }
}

// manages console related things
static class ConsoleManager
{
    static public void NewWindow(string argument)
    {
        ProcessStartInfo info = new() // creates new info
        {
            FileName = "cmd.exe",       // which program to open
            Arguments = $""" /c start "" "{Environment.ProcessPath}" {argument} """,    // finds the correct path and open with argument
        };

        Process.Start(info);
    }

    static public void WriteReadAndClear(params string[] texts) // Clears the text, writes out the block(s), and await enter input
    {
        Console.Clear();
        Console.WriteLine(TextHandler.GetText(texts[0], texts[1..])); // first argument becomes key to acces block, second becomes input variable for that specific block
        Console.ReadLine();
    }
}