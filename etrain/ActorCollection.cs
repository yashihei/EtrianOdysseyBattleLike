namespace Etrain;

public class ActorCollection
{
    private IEnumerable<Actor> players;
    private IEnumerable<Actor> enemies;

    public ActorCollection(IEnumerable<Actor> actors)
    {
        players = actors.Where(actor => actor.IsPc);
        enemies = actors.Where(actor => !actor.IsPc);
    }

    public IEnumerable<Actor> AliveActors()
    {
        return AlivePlayers().Concat(AliveEnemies());
    }

    public IEnumerable<Actor> AlivePlayers()
    {
        return players.Where(player => !player.IsDead());
    }

    public IEnumerable<Actor> AliveEnemies()
    {
        return enemies.Where(enemy => !enemy.IsDead());
    }
}
