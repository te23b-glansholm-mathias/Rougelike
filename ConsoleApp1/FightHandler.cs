class Fight
{
    // starts a battle with 
    public static void BeginBattle(params string[] enemyName)
    {
        float totalHP = 0;
        float ActiveEnemies = 0;

        for (int i = 0; i < enemyName.Length; i++) // creates every enemy
        {
            Common enemy = new(enemyName[i]);
            totalHP += enemy.HP; // adds the new enemy hp to the total
            GameHandler.SetActive(enemy);
        }

        ActiveEnemies = GameHandler.ActiveEnemies!.Count;

        // textblock changes depending on activeEnemies
        ConsoleManager.WriteReadAndClear("enemyBattleBase_" + ActiveEnemies, TextHandler.GetText("fightStart_" + ActiveEnemies));

        while (totalHP > 0) // while any of the enemies are still living
        {
            Console.Clear();

            for (int i = 0; i < GameHandler.ActiveEnemies!.Count; i++) // all enemies take a turn
            {
                GameHandler.ActiveEnemies[i].TakeTurn(GameHandler.ActiveEnemies[i]);
            }

            ConsoleManager.WriteReadAndClear("enemyBattleBase_" + ActiveEnemies, TextHandler.GetText("attackReceive_" + ActiveEnemies));
            GameHandler.ResetAttack(); // cleans up for next turn
        }

        BattleWon();
    }

    static void BattleWon()
    {
        throw new NotImplementedException();
    }
}