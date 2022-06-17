namespace Etrain;

public class Actor
{
    public int Id { get; }
    public string Name { get; }
    public int Hp { get; private set; }
    public int Tp { get; }
    public bool IsPc { get; }

    public Actor(int id, string name, int hp, int tp, bool isPc)
    {
        Id = id;
        Name = name;
        Hp = hp;
        Tp = tp;
        IsPc = isPc;
    }

    public void ApplyHpDamage(int hpDamageValue)
    {
        Hp -= hpDamageValue;
    }

    public bool IsDead()
    {
        return Hp <= 0;
    }
}
