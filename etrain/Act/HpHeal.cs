namespace Etrain;

public class HpHeal : IAct
{
    public Actor Source { get; }
    public Actor Target { get; }
    public int HealValue { get; }

    public HpHeal(Actor source, Actor target, int healValue)
    {
        Source = source;
        Target = target;
        HealValue = healValue;
    }

    public void Execute()
    {
        Target.ApplyHpHeal(HealValue);
    }

    public override string ToString()
    {
        return $"{Target.Name}は{HealValue}回復した！";
    }
}
