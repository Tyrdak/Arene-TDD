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
    public void Attack_ArmorOutweighsForce_DealsNoDamage()
    {
        var attacker = new Gladiator("Spartacus", health: 100, strength: 2, armor: 1);
        var tank = new Gladiator("Murmillo", health: 100, strength: 5, armor: 50);
        var dice = new FixedDice(3);

        attacker.Attack(tank, dice);

        // 3 + 2 - 50 = -45 -> clamp to 0
        Assert.Equal(100, tank.Health);
    }

    [Fact]
    public void Attack_CriticalHit_DoublesDicePlusStrengthBeforeArmor()
    {
        var attacker = new Gladiator("Spartacus", health: 100, strength: 10, armor: 2);
        var defender = new Gladiator("Crixus", health: 100, strength: 8, armor: 4);
        var dice = new FixedDice(6);

        attacker.Attack(defender, dice);

        // (6 + 10) * 2 - 4 = 28
        Assert.Equal(72, defender.Health);
    }

    [Fact]
    public void Attack_DamageExceedsHealth_HealthClampsAtZero()
    {
        var attacker = new Gladiator("Spartacus", health: 100, strength: 50, armor: 2);
        var defender = new Gladiator("Crixus", health: 5, strength: 8, armor: 0);
        var dice = new FixedDice(6);

        attacker.Attack(defender, dice);

        Assert.Equal(0, defender.Health);
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
