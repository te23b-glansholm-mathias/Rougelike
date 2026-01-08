using System.Net.NetworkInformation;

class TextHandler
{
    public static Dictionary<string, string> blocks = new Dictionary<string, string>();

    public static void ReadFile(string lk)
    {
        string allText = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, lk + ".txt"));
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, lk + ".txt");

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

        Console.WriteLine(textBeetwen);
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

        return textBeetwen;
    }
}