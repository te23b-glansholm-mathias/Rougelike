using System.Net.NetworkInformation;

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
        text = text.Replace("{playerName}", Core.playerName);
        text = text.Replace("{playerLevel}", Core.Player!.Level.ToString());
        text = text.Replace("{playerHP}", Core.Player.HP.ToString());
        text = text.Replace("{playerMaxHP}", Core.Player.MaxHP.ToString());
        text = text.Replace("{playerFP}", Core.Player.FP.ToString());
        text = text.Replace("{playerMaxFP}", Core.Player.MaxFP.ToString());
        if (Core.ActiveEnemy != null)
        {
            text = text.Replace("{enemyName}", Core.ActiveEnemy!.name.ToString());
            text = text.Replace("{enemyHP}", Core.ActiveEnemy!.HP.ToString());
            text = text.Replace("{enemyMaxHP}", Core.ActiveEnemy!.MaxHP.ToString());
            text = text.Replace("{enemyFP}", Core.ActiveEnemy!.FP.ToString());
            text = text.Replace("{enemyMaxFP}", Core.ActiveEnemy!.MaxFP.ToString());
            text = text.Replace("{enemyLevel}", Core.ActiveEnemy!.LVL.ToString());

        }
        return text;
    }
}