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
        attacksOwned[Random.Shared.Next(attacksOwned.Count)].Execute();
    }
}

public class Attack
{
    private Action action;
    public string Name { get; }

    public Attack(string name, Action action)
    {
        Name = name;
        this.action = action;
    }

    public void Execute()
    {
        action();
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

    public void Slash()
    {
        GameHandler.SetAttack("Slash", ATK);
        GameHandler.Player!.TakeDamage(GameHandler.ActiveDamage);
    }

    public void Stomp()
    {
        GameHandler.SetAttack("Stomp", ATK + 4);
        GameHandler.Player!.TakeDamage(GameHandler.ActiveDamage);
    }
}
