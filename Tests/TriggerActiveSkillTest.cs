using System;
using Etrain;
using Xunit;

namespace Tests;

public class TriggerActiveSkillTest
{
    [Fact]
    void AttackTypeのスキルで攻撃出来る()
    {
        var skill = new ActiveSkill(0, "testAttack", 10, ActiveSkillFormulaType.Attack);
        var player = new Actor(0, "player1", 100, 0, true, Array.Empty<ActiveSkill>());
        var enemy = new Actor(1, "enemy1", 100, 0, false, Array.Empty<ActiveSkill>());
        var act = new TriggerActiveSkill(player, enemy, skill);

        act.Execute();

        Assert.True(enemy.Hp == enemy.MaxHp - skill.BaseValue);
    }

    [Fact]
    void HealTypeのスキルで回復出来る()
    {
        var skill = new ActiveSkill(1, "testAttack", 10, ActiveSkillFormulaType.Heal);
        var player = new Actor(0, "player1", 100, 0, true, Array.Empty<ActiveSkill>());
        var enemy = new Actor(1, "enemy1", 100, 0, false, Array.Empty<ActiveSkill>());
        enemy.ApplyHpDamage(10);
        var act = new TriggerActiveSkill(player, enemy, skill);

        act.Execute();

        Assert.True(enemy.Hp == enemy.MaxHp);
    }
}
