class MenuManager
{
    static public void ShowMenu()
    {
        bool show = true;

        while (show)
        {
            TextHandler.WriteText("intro_0", GameHandler.PlayerName!);

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.A:
                    show = false;
                    TutorialManager.TutorialRequest();
                    break;

                case ConsoleKey.B:
                    ConsoleManager.NewWindow("settings");
                    break;

                case ConsoleKey.C:
                    ConsoleManager.NewWindow("achievements");
                    break;

                case ConsoleKey.D:
                    Environment.Exit(0);
                    break;
            }

            if (show) Console.Clear();
        }
    }
}