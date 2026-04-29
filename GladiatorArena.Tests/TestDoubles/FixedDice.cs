namespace GladiatorArena.Tests.TestDoubles;

public class FixedDice(int value) : IDice
{
    public int Roll() => value;
}
