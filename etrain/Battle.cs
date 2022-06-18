namespace Etrain;

public class Battle
{
    private int phase = 0;
    private IEnumerable<Actor> actors = new Actor[] {
        new(0, "player1", 100, 0, true),
        new(1, "player2", 100, 0, true),
        new(2, "enemy1", 100, 0, false),
        new(3, "enemy2", 100, 0, false) };
    private ActiveSkill naguru = new(0, "殴る", 10);

    public bool RunPhase()
    {
        phase++;
        Console.WriteLine($"=====================================");
        Console.WriteLine($"phase{phase}");
        Console.WriteLine($"=====================================");

        var players = actors.Where(actor => actor.IsPc).ToArray();
        var enemies = actors.Where(actor => !actor.IsPc).ToArray();
        var aliveEnemies = enemies.Where(enemy => !enemy.IsDead()).ToArray();
        var enemyTexts = aliveEnemies.Select((enemy, idx) => $"[{idx}] {enemy.Name} HP={enemy.Hp}").ToArray();
        var commands = new List<Command>();

        // コマンド入力
        foreach (var player in players)
        {
            Console.WriteLine("誰に攻撃しますか?");
            Console.WriteLine(string.Join(", ", enemyTexts));
            var targetIndex = Math.Clamp(ReadNumber(), 0, aliveEnemies.Length - 1);
            var command = new Command(player, aliveEnemies[targetIndex], naguru);
            commands.Add(command);
        }

        // バトル進行
        foreach (var command in commands)
        {
            var hit = command.Evaluate();
            hit.Execute();
            Console.WriteLine(command.ToString());
            Console.WriteLine(hit.ToString());
        }

        if (enemies.All(enemy => enemy.IsDead()))
        {
            Console.WriteLine("バトルに勝利した！");
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
