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

    public IAct Evaluate()
    {
        return new TriggerActiveSkill(Source, Target, ActiveSkill);
    }
}
