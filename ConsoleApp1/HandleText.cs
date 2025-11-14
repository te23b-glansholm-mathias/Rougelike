class TextHandler
{
    public static void WriteText(int block, string? lk)
    {
        string allText;
        string startText;
        string stopText;

        allText = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, lk + ".txt"));
        startText = "[" + "BLOCK " + block + "]";
        stopText = "[" + "END " + block + "]";

        if (allText.Contains(startText) && allText.Contains(stopText))
        {
            string textBeetwen;
            int start, end;
            start = allText.LastIndexOf(startText);
            end = allText.IndexOf(stopText);

            textBeetwen = allText.Substring(start, end - start);

            Console.WriteLine(textBeetwen);
        }
        else Console.WriteLine("ERROR: could not find block");
    }
}