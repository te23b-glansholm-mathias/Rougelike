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
            // start becomes index of the first block (after pos)
            start = allText.IndexOf(startText, pos);
            if (start == -1) break;

            //  endOfStart becomes index of the endText string (after start)
            endOfStart = allText.IndexOf(endOfStartText, start);
            if (endOfStart == -1) break;

            // end becomes index of the endText (after start)
            end = allText.IndexOf(endText, start);
            if (end == -1) break;

            // key becomes text beetwen startText and endOfStartText without whitespaces
            string key = allText[(start + startText.Length)..endOfStart].Trim();

            // textBeetwen becomes the endOfStart and end index without whitespaces
            string textBeetwen = allText[(endOfStart + 1)..end].Trim();

            blocks.Add(key, textBeetwen);
            pos = end + endText.Length; // changes the position to the end of the last block
        }
    }

    // writes the text associated with the key
    public static string GetText(string key, params string[] args) // the overload strings replaces the numbered variables in the block
    {
        if (!blocks.TryGetValue(key, out string? textBeetwen)) // if the block couldn't be found
        {
            throw new ArgumentException($"Couldn't find block '{key}'");
        }

        for (int i = 0; i < args.Length; i++) // for every argument
        {
            textBeetwen = textBeetwen.Replace("{" + (i + 1) + "}", args[i]);    //replaces the {i} with the argument
        }

        return GetFinalText(textBeetwen);
    }

    static string GetFinalText(string text) // replaces some variables
    {
        text = text.Replace("{playerName}", GameHandler.PlayerName);
        text = text.Replace("{playerLevel}", GameHandler.Player!.Level.ToString());
        text = text.Replace("{playerHP}", GameHandler.Player.HP.ToString());
        text = text.Replace("{playerMaxHP}", GameHandler.Player.MaxHP.ToString());
        text = text.Replace("{playerFP}", GameHandler.Player.FP.ToString());
        text = text.Replace("{playerMaxFP}", GameHandler.Player.MaxFP.ToString());
        if (GameHandler.ActiveEnemies != null) // if the activeEnemy is defined
        {
            for (int i = 0; i < GameHandler.ActiveEnemies.Count; i++) // make sure every enemies block variables get replaced
            {
                text = text.Replace("{enemyName" + (i + 1) + "}", GameHandler.ActiveEnemies[i].Name?.ToString());
                text = text.Replace("{enemyHP" + (i + 1) + "}", GameHandler.ActiveEnemies[i].HP.ToString());
                text = text.Replace("{enemyMaxHP" + (i + 1) + "}", GameHandler.ActiveEnemies[i].MaxHP.ToString());
                text = text.Replace("{enemyFP" + (i + 1) + "}", GameHandler.ActiveEnemies[i].FP.ToString());
                text = text.Replace("{enemyMaxFP" + (i + 1) + "}", GameHandler.ActiveEnemies[i].MaxFP.ToString());
                text = text.Replace("{enemyLevel" + (i + 1) + "}", GameHandler.ActiveEnemies[i].LVL.ToString());
            }

            for (int i = 0; i < GameHandler.ActiveAttack!.Count; i++)
            {
                text = text.Replace("{activeAttack" + (i + 1) + "}", GameHandler.ActiveAttack[i]);
            }

            for (int i = 0; i < GameHandler.ActiveDamage.Count; i++)
            {
                if (GameHandler.ActiveDamage[i] > 0) text = text.Replace("{damageMessage" + (i + 1) + "}", $"for {GameHandler.ActiveDamage[i]} damage ");
                else text = text.Replace("{damageMessage" + (i + 1) + "}", ""); // replaces the damagemessage block variable to nothing if the enemy did no damage
            }
        }
        return text;
    }
}