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

    [Fact]
    public void Attack_ThrowsWhenGladiatorTargetsItself()
    {
        var spartacus = new Gladiator("Spartacus", health: 100, strength: 10, armor: 2);
        var dice = new FixedDice(3);

        var act = () => spartacus.Attack(spartacus, dice);

        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void Attack_StandardHit_DefenderLosesDicePlusStrengthMinusArmor()
    {
        var attacker = new Gladiator("Spartacus", health: 100, strength: 10, armor: 2);
        var defender = new Gladiator("Crixus", health: 100, strength: 8, armor: 4);
        var dice = new FixedDice(3);

        attacker.Attack(defender, dice);

        // 3 (dice) + 10 (strength) - 4 (armor) = 9
        Assert.Equal(91, defender.Health);
    }

    [Fact]
    public void Attack_TwoConsecutiveStandardHits_DamageStacks()
    {
        var attacker = new Gladiator("Spartacus", health: 100, strength: 10, armor: 2);
        var defender = new Gladiator("Crixus", health: 100, strength: 8, armor: 4);
        var dice = new SequenceDice(3, 5);

        attacker.Attack(defender, dice);
        attacker.Attack(defender, dice);

        // 1st: 3 + 10 - 4 =  9 -> 91
        // 2nd: 5 + 10 - 4 = 11 -> 80
        Assert.Equal(80, defender.Health);
    }
}
