// See https://aka.ms/new-console-template for more information

using Etrain;

var battle = new Battle();

var actors = new Actor[] {
    new(0, "player1", 100, 0, true),
    new(1, "player2", 100, 0, true),
    new(2, "enemy1", 100, 0, false),
    new(3, "enemy2", 100, 0, false) };

battle.EnterActors(actors);

while (!battle.IsEnd())
{
    battle.InputCommandsByConsole();
    battle.ProgressPhase();
}

// TODO: 敗北対応
Console.WriteLine("バトルに勝利した！");
