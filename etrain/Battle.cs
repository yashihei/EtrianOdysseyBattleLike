using System.Diagnostics;

namespace Etrain;

public class Battle
{
    private static ActiveSkill naguru = new(0, "殴る", 10);

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

    public void InputCommandsByConsole()
    {
        var aliveEnemies = actorCollection.AliveEnemies().ToArray();
        var enemyTexts = aliveEnemies.Select((enemy, idx) => $"[{idx}] {enemy.Name} HP={enemy.Hp}").ToArray();

        Console.WriteLine($"=====================================");
        Console.WriteLine($"turn{turn}");
        Console.WriteLine($"=====================================");

        foreach (var player in actorCollection.AlivePlayers())
        {
            Console.WriteLine("誰に攻撃しますか?");
            Console.WriteLine(string.Join(", ", enemyTexts));
            var targetIndex = Math.Clamp(ReadNumber(), 0, aliveEnemies.Length - 1);
            var command = new Command(player, aliveEnemies[targetIndex], naguru);
            commands.Add(command);
        }
    }

    public void ProgressTurn()
    {
        commands.AddRange(CalculateEnemiesCommand(actorCollection));

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

    public bool IsEnd()
    {
        // FIXME: 敗北を判定する
        return !actorCollection.AliveEnemies().Any();
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
