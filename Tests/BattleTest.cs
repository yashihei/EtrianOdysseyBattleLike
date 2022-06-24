using Etrain;
using Xunit;

namespace Tests;

public class BattleTest
{
    [Fact]
    public void 敵を全て倒すと勝利する()
    {
        var battle = new Battle();

        var testSkill = new ActiveSkill(0, "test", 10);
        var player = new Actor(0, "player1", 100, 0, true, new []{ testSkill });
        var enemy = new Actor(1, "enemy1", 10, 0, false, new []{ testSkill });
        var actors = new[]
        {
            player, enemy
        };
        battle.EnterActors(actors);

        var commands = new Command[]
        {
            new(player, enemy, testSkill),
            new(enemy, player, testSkill),
        };
        battle.InputCommands(commands);

        battle.ProgressTurn();

        Assert.True(battle.GetResult() == Battle.Result.Win);
    }

    [Fact]
    public void プレイヤーが全て倒されると敗北する()
    {
        var battle = new Battle();

        var testSkill = new ActiveSkill(0, "test", 10);
        var player = new Actor(0, "player1", 10, 0, true, new []{ testSkill });
        var enemy = new Actor(1, "enemy1", 100, 0, false, new []{ testSkill });
        var actors = new[]
        {
            player, enemy
        };
        battle.EnterActors(actors);

        var commands = new Command[]
        {
            new(player, enemy, testSkill),
            new(enemy, player, testSkill),
        };
        battle.InputCommands(commands);

        battle.ProgressTurn();

        Assert.True(battle.GetResult() == Battle.Result.Lose);
    }
}
