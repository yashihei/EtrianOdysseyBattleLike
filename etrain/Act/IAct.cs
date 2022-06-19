namespace Etrain;

public interface IAct
{
    public Actor Source { get; }
    public void Execute();
}
