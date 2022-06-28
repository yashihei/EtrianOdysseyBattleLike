// See https://aka.ms/new-console-template for more information

using Etrain;

var battle = new Battle();

var bokoboko = new ActiveSkill(0, "ぼこぼこ殴る", 10, ActiveSkillFormulaType.Attack, ActiveSkillTargetType.Enemy);
var chotto = new ActiveSkill(1, "ちょっと殴る", 5, ActiveSkillFormulaType.Attack, ActiveSkillTargetType.Enemy);
var heal = new ActiveSkill(2, "ヒール", 100, ActiveSkillFormulaType.Heal, ActiveSkillTargetType.Friend);
var actors = new Actor[] {
    new(0, "player1", 100, 0, true, new []{ bokoboko, heal }),
    new(1, "player2", 100, 0, true, new []{ bokoboko }),
    new(2, "enemy1", 100, 0, false, new []{ chotto }),
    new(3, "enemy2", 100, 0, false, new []{ chotto }) };

battle.EnterActors(actors);

while (battle.GetResult() == Battle.Result.Undecided)
{
    battle.InputPlayerCommandsByConsole();
    battle.InputEnemyCommandsByAI();
    battle.ProgressTurn();
}

if (battle.GetResult() == Battle.Result.Win)
{
    Console.WriteLine("バトルに勝利した！");
}
if (battle.GetResult() == Battle.Result.Lose)
{
    Console.WriteLine("バトルに敗北した…");
}
