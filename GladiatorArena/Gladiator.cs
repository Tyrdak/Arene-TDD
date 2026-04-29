namespace GladiatorArena;

public class Gladiator(string name, int health, int strength, int armor)
{
    private const int MinDiceFace = 1;
    private const int MaxDiceFace = 6;

    public string Name { get; } = name;
    public int Health { get; private set; } = health;
    public int Strength { get; private set; } = strength;
    public int Armor { get; private set; } = armor;

    public void Attack(Gladiator opponent, IDice dice)
    {
        var score = dice.Roll();
        EnsureLegalDiceRoll(score);
    }

    private static void EnsureLegalDiceRoll(int score)
    {
        if (score is < MinDiceFace or > MaxDiceFace)
            throw new ArgumentOutOfRangeException(
                nameof(score), score, $"Dice roll must be between {MinDiceFace} and {MaxDiceFace}.");
    }
}
