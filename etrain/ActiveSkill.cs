namespace Etrain;

public enum ActiveSkillFormulaType
{
    Attack, Heal
}

public enum ActiveSkillTargetType
{
    Friend, Enemy
}

public class ActiveSkill
{
    public int Id { get; }
    public string Name { get; }
    public int BaseValue { get; }
    public ActiveSkillFormulaType FormulaType { get; }
    public ActiveSkillTargetType TargetType { get; }

    public ActiveSkill(int id, string name, int baseValue, ActiveSkillFormulaType formulaType, ActiveSkillTargetType targetType)
    {
        Id = id;
        Name = name;
        BaseValue = baseValue;
        FormulaType = formulaType;
        TargetType = targetType;
    }
}
