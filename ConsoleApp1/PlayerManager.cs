
public class Character
{
    public float Level;
    public float HP;
    public float MaxHP;
    public float Defense;
    public float ATK;
    public float FP;
    public float MaxFP;
    
    public void TakeDamage(float amount)
    {
        HP -= amount;
    }
}
