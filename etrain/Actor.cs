namespace Etrain;

public class Actor
{
    public int Id { get; }
    public string Name { get; }
    public int Hp { get; private set; }
    public int Tp { get; }
    public bool IsPc { get; }
    public IEnumerable<ActiveSkill> ActiveSkills { get; }

    public Actor(int id, string name, int hp, int tp, bool isPc, IEnumerable<ActiveSkill> activeSkills)
    {
        Id = id;
        Name = name;
        Hp = hp;
        Tp = tp;
        IsPc = isPc;
        ActiveSkills = activeSkills;
    }

    public void ApplyHpDamage(int hpDamageValue)
    {
        Hp -= hpDamageValue;
        Hp = Math.Max(Hp, 0);
    }

    public bool IsDead()
    {
        return Hp <= 0;
    }
}
