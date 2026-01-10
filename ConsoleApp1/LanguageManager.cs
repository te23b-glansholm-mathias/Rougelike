class LanguageManager
{
    public static string languageKey = "En";

    public static void ChooseLanguage()
    {
        Console.WriteLine("Choose language / 言語を選択");

        while (true)
        {
            Console.WriteLine(
            """
            a - English
            b - 日本語
            c - Brainrot
            """);

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.A:
                    languageKey = "En";
                    break;

                case ConsoleKey.B:
                    languageKey = "Jp";
                    break;

                case ConsoleKey.C:
                    languageKey = "Br";
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Please choose a valid language");
                    continue;
            }

            TextHandler.ReadFile(languageKey);
            break;
        }
    }
}