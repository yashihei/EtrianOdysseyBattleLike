using System.Diagnostics;

namespace Etrain;

public class Battle
{
    public enum Result
    {
        Win, Lose, Undecided
    }

    private int turn = 1;
    private ActorCollection actorCollection;
    private List<Command> commands = new();

    public void EnterActors(IEnumerable<Actor> actors)
    {
        actorCollection = new ActorCollection(actors);
    }

    public void InputCommands(IEnumerable<Command> commands)
    {
        this.commands.AddRange(commands);
    }

    public void InputPlayerCommandsByConsole()
    {
        Console.WriteLine($"=====================================");
        Console.WriteLine($"turn{turn}");
        Console.WriteLine(actorCollection.AlivePlayersText());
        Console.WriteLine(actorCollection.AliveEnemiesText());
        Console.WriteLine($"=====================================");

        var aliveEnemies = actorCollection.AliveEnemies().ToArray();
        foreach (var player in actorCollection.AlivePlayers())
        {
            Console.WriteLine($"{player.Name}は誰に攻撃しますか?");
            var targetIndex = Math.Clamp(ReadNumber(), 0, aliveEnemies.Length - 1);
            var naguru = new ActiveSkill(0, "ぼこぼこ殴る", 10);
            var command = new Command(player, aliveEnemies[targetIndex], naguru);
            commands.Add(command);
        }
    }

    public void InputEnemyCommandsByAI()
    {
        commands.AddRange(CalculateEnemiesCommand(actorCollection));
    }

    public void ProgressTurn()
    {
        Debug.Assert(ValidateCommands(), "Invalid command!!");

        foreach (var command in commands)
        {
            var result = command.Evaluate();
            result.Execute();
            Console.WriteLine(command.ToString());
            Console.WriteLine(result.ToString());
        }

        commands.Clear();
        turn++;
    }

    public Result GetResult()
    {
        // 敵が全滅
        if (!actorCollection.AliveEnemies().Any())
        {
            return Result.Win;
        }

        // プレイヤー達が全滅
        if (!actorCollection.AlivePlayers().Any())
        {
            return Result.Lose;
        }

        return Result.Undecided;
    }

    private bool ValidateCommands()
    {
        if (commands.Count != actorCollection.AliveActors().Count())
        {
            return false;
        }

        // 全ての生存Actorのコマンド発行してるか
        var actorIds = actorCollection.AliveActors().Select(actor => actor.Id);
        var commandSourceIds = commands.Select(command => command.Source.Id);
        if (actorIds.Except(commandSourceIds).Any())
        {
            return false;
        }

        return true;
    }

    private static IEnumerable<Command> CalculateEnemiesCommand(ActorCollection actorCollection)
    {
        var commands = new List<Command>();
        var players = actorCollection.AlivePlayers().ToArray();
        var naguru = new ActiveSkill(1, "ちょっと殴る", 5);

        foreach (var enemy in actorCollection.AliveEnemies())
        {
            // とりあえずランダム攻撃させてる
            var targetIndex = new Random().Next(0, players.Length);
            var target = players[targetIndex];
            commands.Add(new Command(enemy, target, naguru));
        }

        return commands;
    }

    private static int ReadNumber()
    {
        while (true)
        {
            var success = int.TryParse(Console.ReadLine(), out var number);
            if (success)
            {
                return number;
            }

            Console.WriteLine("数値を入力してください");
        }
    }
}
