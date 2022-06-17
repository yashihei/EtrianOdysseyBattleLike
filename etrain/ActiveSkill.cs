namespace Etrain;

public class ActiveSkill
{
    public int Id { get; }
    public string Name { get; }
    public int DamageValue { get; }

    public ActiveSkill(int id, string name, int damageValue)
    {
        Id = id;
        Name = name;
        DamageValue = damageValue;
    }
}
