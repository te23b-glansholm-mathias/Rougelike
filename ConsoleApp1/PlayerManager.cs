public class Character
{
    public float Level;
    private float _hp;
    public float MaxHP;
    protected float Defense;
    protected float ATK;
    public float FP;
    public float MaxFP;

    public Character(string which)
    {
        switch (which)
        {
            case "Warrior":
                Level = 1;
                _hp = 100;
                MaxHP = 120;
                ATK = 25;
                FP = 0;
                MaxFP = 0;
                break;
        }
    }

    public float HP
    {
        get { return _hp; }
        set
        {
            if (value < 0) value = 0;       
            if (value > MaxHP) value = MaxHP;  
            _hp = value;                       
        }
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;
    }
}
