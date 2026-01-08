public class Enemy
{
    public string name = "";
    public float HP; //base
    public float maxHP; //base
    public float FP = 0; //base
    public float maxFP = 0; //base
    public float ATK; //base
    public float LVL = 1;
}

public class Common : Enemy
{
    public void Slash()
    {
        Core.Player!.TakeDamage(ATK);
    }
}

