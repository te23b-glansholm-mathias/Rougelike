public abstract class Enemy
{
    public string? Name { get; protected set; }
    public float HP { get; protected set; }
    public float MaxHP { get; protected set; }
    public float FP { get; protected set; }
    public float MaxFP { get; protected set; }
    public float ATK { get; protected set; }
    public float LVL { get; protected set; }

    protected List<Attack> attacksOwned = new();

    public virtual void DoAction()
    {
        attacksOwned[Random.Shared.Next(attacksOwned.Count)].Execute(ATK);
    }
}

public delegate void AttackFunc(float båt);

public class Attack
{
    private AttackFunc fredBoat;
    public string Name { get; }

    public Attack(string name, AttackFunc action)
    {
        Name = name;
        this.fredBoat = fredBoat;
    }

    public void Execute(float ATk)
    {
        fredBoat(ATk);
    }
}
    
public class Common : Enemy
{
    public Common(string enemyName)
    {
        Name = enemyName;

        if (Name == "Frog")
        {
            HP = 80;
            ATK = 6;

            attacksOwned.Add(new Attack("Slash", Slash));
            attacksOwned.Add(new Attack("Stomp", Stomp));
        }
    }

    public AttackFunc Slash = (ATK) =>
    {
        GameHandler.SetAttack("Slash", ATK);
        GameHandler.Player!.TakeDamage(GameHandler.ActiveDamage);
    };

    public AttackFunc Stomp = (ATK) =>
    {
        GameHandler.SetAttack("Slash", ATK + 4);
        GameHandler.Player!.TakeDamage(GameHandler.ActiveDamage);
    };
}

public class Uncommon : Enemy
{
    public Uncommon(string enemyName)
    {
        Name = enemyName;

        if (Name == "Skeleton")
        {
            HP = 190;
            ATK = 15;
        }
    }
}