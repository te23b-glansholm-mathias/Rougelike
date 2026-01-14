public delegate void AttackDelegate(Enemy enemy); // delegate for enemy attacks

public abstract class Enemy // base class for all enemies
{
    public string? Name { get; protected set; }
    public float HP { get; protected set; }
    public float MaxHP { get; protected set; }
    public float FP { get; protected set; }
    public float MaxFP { get; protected set; }
    public float ATK { get; protected set; }
    public float LVL { get; protected set; }

    // list of attacks the enemy have including weight for attack bias
    protected List<(AttackDelegate attack, int weight)> attacksOwned = [];

    // every active enemy must have their own turn
    public abstract void TakeTurn(Enemy enemy);

    public void DoAction(AttackDelegate attack, Enemy enemy) // classic delegate things
    {
        attack(enemy);
    }
    
    public void Heal(float amount)
    {
        HP += amount;
    }

    public void Buff(float amount)
    {
        ATK += amount;
    }
}

public class Common : Enemy
{
    public Common(string enemyName)
    {
        Name = enemyName;

        if (Name == "Frog") // should probably make use of serialization at some point
        {
            HP = 80;
            ATK = 6;

            attacksOwned.Add((Attacks.Slash, 10));
            attacksOwned.Add((Attacks.Stomp, 10));
        }

        if (Name == "Bird")
        {
            HP = 50;
            ATK = 3;

            attacksOwned.Add((Attacks.Scream, 80));
            attacksOwned.Add((Attacks.Slash, 10));
            attacksOwned.Add((Attacks.Swipe, 10));
        }
    }

    public override void TakeTurn(Enemy enemy)
    {
        chooseAttack(enemy); 
    }

    // chooses attack with higher weight leading to higher chance of being choosen
    public void chooseAttack(Enemy enemy)
    {
        int totalWeight = 0;

        // sum of all attack weights
        foreach (var attack in attacksOwned) totalWeight += attack.weight;
        int rng = Random.Shared.Next(totalWeight);

        foreach (var attack in attacksOwned) // shenanigans 
        {
            rng -= attack.weight;
            if (rng <= 0)
            {
                DoAction(attack.attack, enemy);
                return;
            }
        }
    }
}

public class Attacks // class for containing all the attacks
{
    static public AttackDelegate Slash = (Enemy enemy) =>
    {
        float finalDamage = enemy.ATK;
        GameHandler.SetAttack("Slash", finalDamage);
        GameHandler.Player!.TakeDamage(finalDamage);
    };

    static public AttackDelegate Stomp = (Enemy enemy) =>
    {
        float finalDamage = enemy.ATK + 4;
        GameHandler.SetAttack("Stomp", finalDamage);
        GameHandler.Player!.TakeDamage(finalDamage);
    };

    static public AttackDelegate Swipe = (Enemy enemy) =>
    {
        float finalDamage = enemy.ATK + 2;
        GameHandler.SetAttack("Swipe", finalDamage);
        GameHandler.Player!.TakeDamage(finalDamage);
    };

    static public AttackDelegate Scream = (Enemy enemy) =>
    {
        GameHandler.SetAttack("Scream");
        enemy.Buff(10);
    };
}