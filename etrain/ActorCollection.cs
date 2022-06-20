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

    public string AlivePlayersText()
    {
        var playerTexts = AlivePlayers().Select((player, idx) => $"[{idx}] {player.Name} HP={player.Hp}");
        return string.Join(", ", playerTexts);
    }

    public string AliveEnemiesText()
    {
        var enemyTexts = AliveEnemies().Select((enemy, idx) => $"[{idx}] {enemy.Name} HP={enemy.Hp}");
        return string.Join(", ", enemyTexts);
    }
}
