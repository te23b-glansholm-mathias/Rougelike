class TextHandler
{
    public static Dictionary<string, string> blocks = new Dictionary<string, string>();

    public static void ReadFile(string lk)
    {
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, lk + ".txt");
        string allText = File.ReadAllText(path);
        AddToDict(allText);
    }

    public static void AddToDict(string allText)
    {
        int pos = 0;
        string startText = "[BLOCK ";
        string endOfStartText = "]";
        string endText = "[END]";

        int start, end, trueEnd;

        while (true)
        {
            start = allText.IndexOf(startText, pos);
            if (start == -1) break;

            end = allText.IndexOf(endOfStartText, start);
            if (end == -1) break;

            trueEnd = allText.IndexOf(endText, start);
            if (trueEnd == -1) break;

            string key = allText.Substring(start + startText.Length, end - (start + startText.Length)).Trim();
            string textBeetwen = allText.Substring(end + 1, trueEnd - (end + 1)).Trim();

            blocks.Add(key, textBeetwen);
            pos = trueEnd + endText.Length;
        }
    }

    public static void WriteText(string key, params string[] args)
    {
        if (!blocks.TryGetValue(key, out string? textBeetwen))
        {
            Console.WriteLine($"Couldn't find block '{key}'");
            return;
        }

        for (int i = 0; i < args.Length; i++)
        {
            textBeetwen = textBeetwen.Replace("{" + (i + 1) + "}", args[i]);
        }

        Console.WriteLine(GetFinalText(textBeetwen));
    }

    public static string GetBlockText(string key, params string[] args)
    {
        if (!blocks.TryGetValue(key, out string? textBeetwen))
        {
            return " ERROR ";
        }

        for (int i = 0; i < args.Length; i++)
        {
            textBeetwen = textBeetwen.Replace("{" + (i + 1) + "}", args[i]);
        }

        return GetFinalText(textBeetwen);
    }

    static string GetFinalText(string text)
    {
        text = text.Replace("{playerName}", GameHandler.PlayerName);
        text = text.Replace("{playerLevel}", GameHandler.Player!.Level.ToString());
        text = text.Replace("{playerHP}", GameHandler.Player.HP.ToString());
        text = text.Replace("{playerMaxHP}", GameHandler.Player.MaxHP.ToString());
        text = text.Replace("{playerFP}", GameHandler.Player.FP.ToString());
        text = text.Replace("{playerMaxFP}", GameHandler.Player.MaxFP.ToString());
        if (GameHandler.ActiveEnemy != null)
        {
            text = text.Replace("{enemyName}", GameHandler.ActiveEnemy!.name.ToString());
            text = text.Replace("{enemyHP}", GameHandler.ActiveEnemy!.HP.ToString());
            text = text.Replace("{enemyMaxHP}", GameHandler.ActiveEnemy!.MaxHP.ToString());
            text = text.Replace("{enemyFP}", GameHandler.ActiveEnemy!.FP.ToString());
            text = text.Replace("{enemyMaxFP}", GameHandler.ActiveEnemy!.MaxFP.ToString());
            text = text.Replace("{enemyLevel}", GameHandler.ActiveEnemy!.LVL.ToString());
            text = text.Replace("{activeAttack}", GameHandler.ActiveAttack);
            text = text.Replace("{activeDamage}", GameHandler.ActiveDamage.ToString());

        }
        return text;
    }
}