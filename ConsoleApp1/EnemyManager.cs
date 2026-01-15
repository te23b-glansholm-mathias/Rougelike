public delegate void AttackDelegate(Enemy enemy); // delegate for enemy attacks

public abstract class Enemy // base class for all enemies
{
    public string? Name { get; protected set; }
    private float _hp;
    public float MaxHP { get; protected set; }
    public float FP { get; protected set; }
    public float MaxFP { get; protected set; }
    public float ATK { get; protected set; }
    public float LVL { get; protected set; }

    public float HP
    {
        get => _hp;
        protected set
        {
            if (value <= 0) OnDeath();
            _hp = Math.Clamp(value, 0, MaxHP);   //hp can never go lower than 0 or higher than maxHp
        }
    }

    // list of attacks the enemy have including weight for attack bias
    protected List<(AttackDelegate attack, int weight)> attacksOwned = [];

    // every active enemy must have their own turn
    public abstract void TakeTurn(Enemy enemy);

    public abstract void OnDeath();

    public void TakeDamage(Enemy enemy, float amount)
    {
        HP -= amount;
    }

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
            MaxHP = 120;
            HP = 80;
            ATK = 6;

            attacksOwned.Add((Attacks.Slash, 10));
            attacksOwned.Add((Attacks.Stomp, 10));
        }

        if (Name == "Bird")
        {
            MaxHP = 70;
            HP = 50;
            ATK = 3;

            attacksOwned.Add((Attacks.Scream, 80));
            attacksOwned.Add((Attacks.Slash, 10));
            attacksOwned.Add((Attacks.Swipe, 10));
        }
    }

    public override void TakeTurn(Enemy enemy)
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

    public override void OnDeath()
    {
        GameHandler.ActiveEnemies!.Remove(this);
    }
}

public class Skeleton : Enemy
{
    public bool canRevive;
    bool IsDead = false;
    protected List<(AttackDelegate attack, int weight)> AttacksOnDead = []; // List of attacks the skeleton can use when dead

    public Skeleton(string enemyName)
    {
        Name = enemyName;

        if (Name == "Skeleton")
        {
            canRevive = true;
            MaxHP = 80;
            HP = 80;
            ATK = 8;

            attacksOwned.Add((Attacks.Slash, 10));
            attacksOwned.Add((Attacks.Stomp, 10));

            AttacksOnDead.Add((Attacks.Slap, 40));
            AttacksOnDead.Add((Attacks.Revive, 10));
        }
    }

    public override void TakeTurn(Enemy enemy)
    {
        if (IsDead == false)
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

        if (IsDead && canRevive)
        {
            int totalWeight = 0;

            // sum of all the attack weights which can be used when the enemy is dead
            foreach (var attack in AttacksOnDead) totalWeight += attack.weight;
            int rng = Random.Shared.Next(totalWeight);

            foreach (var attack in AttacksOnDead) // shenanigans 
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

    public override void OnDeath()
    {
        IsDead = true;
        if (GameHandler.ActiveEnemies!.Count == 1) GameHandler.ActiveEnemies!.Remove(this);
    }
}

public class Attacks // class for containing all the attacks
{
    // common attacks
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

    // rarer attacks
    static public AttackDelegate BoneSlam = (Enemy enemy) =>
    {
        float finalDamage = (float)(enemy.ATK * 1.4);
        GameHandler.SetAttack("BoneSlam", finalDamage);
        GameHandler.Player!.TakeDamage(finalDamage);
    };

    static public AttackDelegate Slap = (Enemy enemy) =>
    {
        float finalDamage = (float)(enemy.ATK * 0.2);
        GameHandler.SetAttack("Slap", finalDamage);
        GameHandler.Player!.TakeDamage(finalDamage);
    };

    static public AttackDelegate Revive = (Enemy enemy) =>
    {
        enemy.Heal(enemy.MaxHP / 2);
        GameHandler.SetAttack("Revive");
    };
}