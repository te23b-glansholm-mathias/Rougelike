class Fight
{
    // starts a battle with 
    public static void BeginBattle(params (string enemyName, string Type)[] enemies)
    {
        GameHandler.ResetForFight();

        float totalHP = 0;

        for (int i = 0; i < enemies.Length; i++) // creates every enemy
        {
            Enemy enemy = enemies[i].Type == "Skeleton" ? new Skeleton(enemies[i].enemyName) : new Common(enemies[i].enemyName);
            totalHP += enemy.HP; // adds the new enemy hp to the total
            GameHandler.SetActive(enemy);
        }

        // textblock changes depending on activeEnemies
        ConsoleManager.WriteReadAndClear("enemyBattleBase_" + GameHandler.ActiveEnemies!.Count, TextHandler.GetText("fightStart_" + GameHandler.ActiveEnemies.Count));

        while (GameHandler.ActiveEnemies.Count > 0) // while any of the enemies are still living
        {
            Console.Clear();

            foreach (Enemy enemy in GameHandler.ActiveEnemies)
            {
                enemy.TakeTurn(enemy);
            }

            ConsoleManager.WriteReadAndClear("enemyBattleBase_" + GameHandler.ActiveEnemies.Count, TextHandler.GetText("attackReceive_" + GameHandler.ActiveEnemies.Count));
            GameHandler.ResetAttack(); // cleans up for next turn

            if (GameHandler.ActiveEnemies.Count > 0)
            {
                // for the player to fight backy
                int whichEnemy = Random.Shared.Next(GameHandler.ActiveEnemies.Count); // chooses random enemy
                float damage = Random.Shared.Next(1, 100);

                Enemy targetEnemy = GameHandler.ActiveEnemies[whichEnemy];

                GameHandler.Player!.Attack(targetEnemy, damage);

                if (GameHandler.ActiveEnemies.Count > 0) ConsoleManager.WriteReadAndClear("enemyBattleBase_" + GameHandler.ActiveEnemies.Count, TextHandler.GetText("attackGive", damage.ToString(), targetEnemy.Name!));
            }
        }

        BattleWon();
    }

    static void BattleWon()
    {
        Console.WriteLine($"You won the game. Nice work!!!!11!!!1111! båt");
        Console.ReadLine();
        ConsoleManager.NewWindow("");
        Environment.Exit(0);
    }
}