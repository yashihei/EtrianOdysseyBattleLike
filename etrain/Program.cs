// See https://aka.ms/new-console-template for more information

using Etrain;

// バトル初期化
var actors = new Actor[] {
    new Actor(0, "player1", 100, 0, true),
    new Actor(1, "player2", 100, 0, true),
    new Actor(2, "enemy1", 100, 0, false),
    new Actor(3, "enemy2", 100, 0, false) };

var naguru = new ActiveSkill(0, "殴る", 10);
var phase = 0;

while (true)
{
    phase++;
    Console.WriteLine($"=====================================");
    Console.WriteLine($"phase{phase}");
    Console.WriteLine($"=====================================");

    var players = actors.Where(actor => actor.IsPc).ToArray();
    var enemies = actors.Where(actor => !actor.IsPc).ToArray();
    var enemyTexts = enemies.Select((enemy, idx) => $"[{idx}] {enemy.Name} HP={enemy.Hp}").ToArray();
    var commands = new List<Command>();

    // コマンド入力
    foreach (var player in players)
    {
        Console.WriteLine("誰に攻撃しますか?");
        Console.WriteLine(string.Join(", ", enemyTexts));
        var targetIndex = Math.Clamp(int.Parse(Console.ReadLine() ?? "0"), 0, enemies.Length - 1);
        var command = new Command(player, enemies[targetIndex], naguru);
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
        break;
    }
}
