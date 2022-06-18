using Etrain;
using Xunit;

namespace Tests;

public class BattleTest
{
    [Fact]
    public void バトルが動作する()
    {
        var battle = new Battle();

        var player = new Actor(0, "player1", 10, 0, true);
        var enemy = new Actor(1, "enemy1", 10, 0, false);
        var actors = new[]
        {
            player, enemy
        };
        battle.EnterActors(actors);

        var testSkill = new ActiveSkill(0, "test", 10);
        var commands = new Command[]
        {
            new(player, enemy, testSkill),
        };
        battle.InputCommands(commands);

        battle.ProgressTurn();

        Assert.True(battle.IsEnd());
    }
}
