namespace GladiatorArena;

public class Gladiator(string name, int health, int strength, int armor)
{
    public string Name { get; } = name;
    public int Health { get; private set; } = health;
    public int Strength { get; private set; } = strength;
    public int Armor { get; private set; } = armor;

    public void Attack(Gladiator opponent, IDice dice)
    {
        var score = dice.Roll();
        if (score is < 1 or > 6)
            throw new ArgumentOutOfRangeException(nameof(dice), score, "Dice roll must be between 1 and 6.");
    }
}
