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
            Console.WriteLine($"{player.Name}の行動");

            var skills = player.ActiveSkills.ToArray();
            var skillsText = string.Join(", ", skills.Select((skill, idx) => $"[{idx}] {skill.Name}"));
            Console.WriteLine("使用するとくぎを選択してください");
            Console.WriteLine(skillsText);
            var skillIndex = ReadNumber(0, skills.Length - 1);

            Console.WriteLine("誰に攻撃しますか?");
            Console.WriteLine(actorCollection.AliveEnemiesText());
            var enemyIndex = ReadNumber(0, aliveEnemies.Length - 1);

            var command = new Command(player, aliveEnemies[enemyIndex], skills[skillIndex]);
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

        // TODO: 所持してるActiveSkillか?

        return true;
    }

    private static IEnumerable<Command> CalculateEnemiesCommand(ActorCollection actorCollection)
    {
        var commands = new List<Command>();
        var players = actorCollection.AlivePlayers().ToArray();
        // FIXME: 所持してるActiveSkillで攻撃する
        var naguru = new ActiveSkill(1, "ちょっと殴る", 5, ActiveSkillFormulaType.Attack);

        foreach (var enemy in actorCollection.AliveEnemies())
        {
            // とりあえずランダム攻撃させてる
            var targetIndex = new Random().Next(0, players.Length);
            var target = players[targetIndex];
            commands.Add(new Command(enemy, target, naguru));
        }

        return commands;
    }

    private static int ReadNumber(int min, int max)
    {
        while (true)
        {
            var readSuccess = int.TryParse(Console.ReadLine(), out var number);
            if (!readSuccess)
            {
                Console.WriteLine("数値を入力してください");
                continue;
            }

            var inRange = number >= min && number <= max;
            if (!inRange)
            {
                Console.WriteLine($"{min}から{max}の範囲内で入力してください");
                continue;
            }

            return number;
        }
    }
}
