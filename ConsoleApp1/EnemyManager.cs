public abstract class Enemy
{
    public string name = "";
    public float HP;
    public float MaxHP;
    public float FP = 0;
    public float MaxFP = 0;
    public float ATK;
    public float LVL = 1;
    
    protected List<Attack> attacksOwned = new();

    public void DoRandomAttack()
    {
        if (attacksOwned.Count == 0) return;
        Random rnd = new();
        int index = rnd.Next(attacksOwned.Count);
        attacksOwned[index].Execute();
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
        name = enemyName;

        if (name == "Frog")
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
