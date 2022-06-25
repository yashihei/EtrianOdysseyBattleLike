namespace Etrain;

public class Actor
{
    public int Id { get; }
    public string Name { get; }
    public int MaxHp { get; }
    public int Hp { get; private set; }
    public int Tp { get; }
    public bool IsPc { get; }
    public IEnumerable<ActiveSkill> ActiveSkills { get; }

    public Actor(int id, string name, int maxHp, int tp, bool isPc, IEnumerable<ActiveSkill> activeSkills)
    {
        Id = id;
        Name = name;
        MaxHp = Hp = maxHp;
        Tp = tp;
        IsPc = isPc;
        ActiveSkills = activeSkills;
    }

    public void ApplyHpDamage(int hpDamageValue)
    {
        Hp -= hpDamageValue;
        Hp = Math.Max(Hp, 0);
    }

    public void ApplyHpHeal(int hpHealValue)
    {
        Hp += hpHealValue;
        Hp = Math.Min(Hp, MaxHp);
    }

    public bool IsDead()
    {
        return Hp <= 0;
    }
}
