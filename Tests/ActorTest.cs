using Etrain;
using Xunit;

namespace Tests;

public class ActorTest
{
    [Fact]
    public void HPが0未満にならない()
    {
        var actor = new Actor(0, "hoge", 100, 0, true);
        actor.ApplyHpDamage(200);

        Assert.False(actor.Hp < 0);
    }
}
