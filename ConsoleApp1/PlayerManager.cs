public class Character
{
    public float Level { get; private set; }
    private float _hp;
    public float MaxHP { get; private set; }
    public float FP { get; private set; }
    public float MaxFP { get; private set; }
    protected float Defense { get; private set; }
    protected float ATK { get; private set; }
    

    public float HP
    {
        get => _hp;
        private set => _hp = Math.Clamp(value, 0, MaxHP);
    }

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

    public void TakeDamage(float amount)
    {
        HP -= amount;
    }
}
