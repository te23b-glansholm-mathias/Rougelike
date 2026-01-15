class TutorialManager
{
    // asks if you wanna play the tutorial
    static public void TutorialRequest()
    {
        Console.Clear();
        Console.WriteLine("Do you want to play the tutorial? [Y/N]");

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Y:
                    StartTutorial();
                    break;

                case ConsoleKey.N:
                    StartTutorial();
                    break;

                case ConsoleKey.Enter:
                    StartTutorial();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Please choose a valid input if you wanna play the tutorial [Y/N]. The Y or N key");
                    continue;
            }
            break;
        }
    }


    static public void StartTutorial() // writes out the text for the tutorial. Check respective language textfile
    {
        ConsoleManager.WriteReadAndClear("battleBase_2", TextHandler.GetText("tutorial_0"), TextHandler.GetText("tutorial_0r2"));
        ConsoleManager.WriteReadAndClear("battleBase_3", TextHandler.GetText("tutorial_1"), TextHandler.GetText("tutorial_1r2"), TextHandler.GetText("tutorial_1r3"));
        ConsoleManager.WriteReadAndClear("battleBase_1", TextHandler.GetText("tutorial_2"));
        ConsoleManager.WriteReadAndClear("battleBase_1", TextHandler.GetText("tutorial_3"));

        Fight.BeginBattle(
            ("Frog", "Common"),
            ("Skeleton", "Skeleton")
        );
    }
}