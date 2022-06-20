// See https://aka.ms/new-console-template for more information

using Etrain;

var battle = new Battle();

var actors = new Actor[] {
    new(0, "player1", 100, 0, true),
    new(1, "player2", 100, 0, true),
    new(2, "enemy1", 100, 0, false),
    new(3, "enemy2", 100, 0, false) };

battle.EnterActors(actors);

while (battle.GetResult() == Battle.Result.Undecided)
{
    battle.InputCommandsByConsole();
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
