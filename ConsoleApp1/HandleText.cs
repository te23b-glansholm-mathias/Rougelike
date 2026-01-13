static class TextHandler
{
    // get blocks of text out of a key
    static Dictionary<string, string> blocks = new();

    public static void ReadFile(string lk)
    {
        // the path to the correct language file (depending on the languagekey)
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, lk + ".txt");

        // sets allText to the files content
        string allText = File.ReadAllText(path);

        BlocksToDict(allText);
    }

    // adds the different blocks into the dictionary
    public static void BlocksToDict(string allText)
    {
        int pos = 0; // current search position
        string startText = "[BLOCK ";
        string endOfStartText = "]";
        string endText = "[END]";

        int start, endOfStart, end; // used while searching

        while (true)
        {
            // sets start to the index of the first block (after pos)
            start = allText.IndexOf(startText, pos);
            if (start == -1) break;

            // sets endOfStart to the index of the endText string (after start)
            endOfStart = allText.IndexOf(endOfStartText, start);
            if (endOfStart == -1) break;

            // sets end to the index of the endText (after start)
            end = allText.IndexOf(endText, start);
            if (end == -1) break;

            // sets key to the 
            string key = allText[(start + startText.Length)..endOfStart].Trim();
            string textBeetwen = allText[(endOfStart + 1)..end].Trim();

            blocks.Add(key, textBeetwen);
            pos = end + endText.Length;
            
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