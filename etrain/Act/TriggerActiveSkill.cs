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
        var hpDamage = new HpDamage(Source, Target, ActiveSkill.DamageValue);
        hpDamage.Execute();
        actLogs.Add(hpDamage.ToString());
    }

    public override string ToString()
    {
        return string.Join("\n", actLogs);
    }
}
