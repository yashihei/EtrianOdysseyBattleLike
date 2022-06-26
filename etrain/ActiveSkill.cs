namespace Etrain;

public enum ActiveSkillFormulaType
{
    Attack, Heal
}

public class ActiveSkill
{
    public int Id { get; }
    public string Name { get; }
    public int BaseValue { get; }
    public ActiveSkillFormulaType FormulaType { get; }

    public ActiveSkill(int id, string name, int baseValue, ActiveSkillFormulaType formulaType)
    {
        Id = id;
        Name = name;
        BaseValue = baseValue;
        FormulaType = formulaType;
    }
}
