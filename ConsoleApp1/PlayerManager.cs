public class Character
{
    public float Level;
    public float HP;
    public float MaxHP;
    public float Defense;
    public float ATK;
    public float FP;
    public float MaxFP;

    public Character(string which)
    {
        switch (which)
        {
            case "Warrior":
                Level = 1;
                HP = 100;
                MaxHP = 120;
                ATK = 25;
                FP = 0;
                MaxFP = 0;
                break;
        }
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
    }
}
