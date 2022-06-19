using System.Diagnostics;

namespace Etrain;

public class Battle
{
    private int turn = 1;
    private IReadOnlyCollection<Actor> players;
    private IReadOnlyCollection<Actor> enemies;
    private List<Command> commands = new();
    private ActiveSkill naguru = new(0, "殴る", 10);

    public void EnterActors(IEnumerable<Actor> actors)
    {
        players = actors.Where(actor => actor.IsPc).ToArray();
        enemies = actors.Where(actor => !actor.IsPc).ToArray();
    }

    public void InputCommands(IEnumerable<Command> commands)
    {
        this.commands.AddRange(commands);
    }

    public void InputCommandsByConsole()
    {
        var aliveEnemies = enemies.Where(enemy => !enemy.IsDead()).ToArray();
        var enemyTexts = aliveEnemies.Select((enemy, idx) => $"[{idx}] {enemy.Name} HP={enemy.Hp}").ToArray();

        Console.WriteLine($"=====================================");
        Console.WriteLine($"turn{turn}");
        Console.WriteLine($"=====================================");

        foreach (var player in players)
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
        return enemies.All(enemy => enemy.IsDead());
    }

    private bool ValidateCommands()
    {
        if (commands.Count != players.Count)
        {
            return false;
        }

        // 全プレイヤーのコマンド発行してるか
        var playerIds = players.Select(player => player.Id);
        var commandSourceIds = commands.Select(command => command.Source.Id);
        if (playerIds.Except(commandSourceIds).Any())
        {
            return false;
        }

        return true;
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
