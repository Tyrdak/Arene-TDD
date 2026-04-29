using GladiatorArena.Tests.TestDoubles;

namespace GladiatorArena.Tests;

public class GladiatorAttackTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(7)]
    [InlineData(-1)]
    public void Attack_ThrowsWhenDiceRollIsOutsideOneToSix(int riggedRoll)
    {
        var attacker = new Gladiator("Spartacus", health: 100, strength: 10, armor: 2);
        var defender = new Gladiator("Crixus", health: 100, strength: 8, armor: 4);
        var dice = new FixedDice(riggedRoll);

        var act = () => attacker.Attack(defender, dice);

        Assert.Throws<ArgumentOutOfRangeException>(act);
    }
}
