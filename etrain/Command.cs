namespace Etrain;

public class Command
{
    public Actor Source { get; }
    public Actor Target { get; }
    public ActiveSkill ActiveSkill { get; }

    public Command(Actor source, Actor target, ActiveSkill activeSkill)
    {
        this.Source = source;
        this.Target = target;
        this.ActiveSkill = activeSkill;
    }

    public Hit Evaluate()
    {
        return new Hit(Source, Target, ActiveSkill.DamageValue);
    }

    public override string ToString()
    {
        return $"{Source.Name}は{ActiveSkill.Name}を使った！";
    }
}
