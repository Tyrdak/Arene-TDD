namespace GladiatorArena;

public class Gladiator(string name, int health, int strength, int armor)
{
    private const int MinDiceFace = 1;
    private const int MaxDiceFace = 6;
    private const int CriticalRoll = MaxDiceFace;

    public string Name { get; } = name;
    public int Health { get; private set; } = health;
    public int Strength { get; private set; } = strength;
    public int Armor { get; private set; } = armor;

    public void Attack(Gladiator opponent, IDice dice)
    {
        EnsureDistinctOpponent(opponent);

        var score = dice.Roll();
        EnsureLegalDiceRoll(score);

        var damage = ComputeDamage(score, opponent.Armor);
        opponent.TakeDamage(damage);
    }

    private int ComputeDamage(int score, int opponentArmor)
    {
        var hitPower = score + Strength;
        if (score == CriticalRoll)
            hitPower *= 2;
        return Math.Max(0, hitPower - opponentArmor);
    }

    private void TakeDamage(int damage)
        => Health = Math.Max(0, Health - damage);

    private void EnsureDistinctOpponent(Gladiator opponent)
    {
        if (ReferenceEquals(this, opponent))
            throw new InvalidOperationException("A gladiator cannot attack itself.");
    }

    private static void EnsureLegalDiceRoll(int score)
    {
        if (score is < MinDiceFace or > MaxDiceFace)
            throw new ArgumentOutOfRangeException(
                nameof(score), score, $"Dice roll must be between {MinDiceFace} and {MaxDiceFace}.");
    }
}
