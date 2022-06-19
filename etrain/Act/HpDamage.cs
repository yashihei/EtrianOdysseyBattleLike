namespace Etrain;

public class HpDamage : IAct
{
    public Actor Source { get; }
    public Actor Target { get; }
    public int DamageValue { get; }

    public HpDamage(Actor source, Actor target, int damageValue)
    {
        Source = source;
        Target = target;
        DamageValue = damageValue;
    }

    public void Execute()
    {
        Target.ApplyHpDamage(DamageValue);
    }

    public override string ToString()
    {
        return $"{Target.Name}に{DamageValue}のダメージ！";
    }
}
