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
        turn++;

        Debug.Assert(ValidateCommands(), "Invalid command!!");

        foreach (var command in commands)
        {
            var hit = command.Evaluate();
            hit.Execute();
            Console.WriteLine(command.ToString());
            Console.WriteLine(hit.ToString());
        }

        commands.Clear();
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

        var commandTargetIds = commands.Select(command => command.Target.Id);
        var enemyIds = enemies.Select(enemy => enemy.Id);
        if (commandTargetIds.Except(enemyIds).Any())
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
