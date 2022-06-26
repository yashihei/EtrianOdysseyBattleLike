namespace Etrain;

public class ActiveSkill
{
    public enum FormulaType
    {
        Attack, Heal
    }

    public int Id { get; }
    public string Name { get; }
    public int BaseValue { get; }
    public FormulaType Type { get; }

    public ActiveSkill(int id, string name, int baseValue, FormulaType type)
    {
        Id = id;
        Name = name;
        BaseValue = baseValue;
        Type = type;
    }
}
