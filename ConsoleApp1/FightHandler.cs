class Fight
{
    public static void BeginBattle(string enemyName)
    {
        Common enemy = new(enemyName);

        GameHandler.SetActive(enemy);

        ConsoleManager.WriteReadAndClear("enemyBattleBase_1", TextHandler.GetText("fightStart"));

        while (enemy.HP > 0)
        {
            Console.Clear();
            enemy.DoAction();
            ConsoleManager.WriteReadAndClear("enemyBattleBase_1", TextHandler.GetText("attackReceive"));
        }

        battleWon();
    }

    static void battleWon()
    {

    }
}