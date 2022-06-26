namespace Etrain;

public class TriggerActiveSkill : IAct
{
    public Actor Source { get; }
    public Actor Target { get; }
    public ActiveSkill ActiveSkill { get; }

    private readonly List<string> actLogs = new();

    public TriggerActiveSkill(Actor source, Actor target, ActiveSkill activeSkill)
    {
        Source = source;
        Target = target;
        ActiveSkill = activeSkill;
    }

    public void Execute()
    {
        actLogs.Add($"{Source.Name}は{ActiveSkill.Name}を使った！");
        IAct act = ActiveSkill.Type switch
        {
            ActiveSkill.FormulaType.Attack => new HpDamage(Source, Target, ActiveSkill.BaseValue),
            ActiveSkill.FormulaType.Heal => new HpHeal(Source, Target, ActiveSkill.BaseValue),
            _ => throw new ArgumentOutOfRangeException()
        };
        act.Execute();
        actLogs.Add(act.ToString());
    }

    public override string ToString()
    {
        return string.Join("\n", actLogs);
    }
}
