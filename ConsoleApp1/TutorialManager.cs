class TutorialManager
{
    static public void TutorialRequest()
    {
        Console.Clear();
        Console.WriteLine("Do you want to play the tutorial? [Y/N] ");

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.Y:
                    StartTutorial();
                    break;

                case ConsoleKey.N:
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Please choose a valid input if you wanna play the tutorial [Y/N]. The Y or N key");
                    continue;
            }
            break;
        }
    }


    static public void StartTutorial()
    {
        Core.WriteReadAndClear("battleBase_2", TextHandler.GetBlockText("tutorial_0"), TextHandler.GetBlockText("tutorial_0r2"));
        Core.WriteReadAndClear("battleBase_3", TextHandler.GetBlockText("tutorial_1"), TextHandler.GetBlockText("tutorial_1r2"), TextHandler.GetBlockText("tutorial_1r3"));
        Core.WriteReadAndClear("battleBase_1", TextHandler.GetBlockText("tutorial_2"));
        Core.WriteReadAndClear("battleBase_1", TextHandler.GetBlockText("tutorial_3"));

        TutorialBattle();
    }

    static void TutorialBattle()
    {
        Common Frog = new("Frog");

        Core.ActiveEnemy = Frog;

        Core.WriteReadAndClear("enemyBattleBase_1", TextHandler.GetBlockText("fightStart"));

        while (true)
        {
            Console.Clear();
            Frog.DoRandomAttack();
            Core.WriteReadAndClear("enemyBattleBase_1", TextHandler.GetBlockText("attackReceive"));
        }
    }
}