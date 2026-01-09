class MenuManager
{
    static public void ShowMenu(string playerName)
    {
        bool show = true;

        while (show)
        {
            TextHandler.WriteText("intro_0", playerName!);

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.A:
                    show = false;
                    TutorialManager.TutorialRequest();
                    break;

                case ConsoleKey.B:
                    Core.NewWindow("settings");
                    break;

                case ConsoleKey.C:
                    Core.NewWindow("achievements");
                    break;

                case ConsoleKey.D:
                    Environment.Exit(0);
                    break;
            }

            if (show) Console.Clear();
        }
    }
}