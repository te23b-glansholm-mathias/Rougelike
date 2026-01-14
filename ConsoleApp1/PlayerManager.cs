public class Character
{
    protected bool IsDead { get; private set; }

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
        private set
        {
            if (value <= 0) GameOver();
            _hp = Math.Clamp(value, 0, MaxHP);   //hp can never go lower than 0 or higher than maxHp
        }
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

    public static void GameOver()
    {
        Console.Clear();
        Console.WriteLine("You died");
        Console.ReadLine();
    }
}
