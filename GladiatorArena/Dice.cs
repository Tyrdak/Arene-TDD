namespace GladiatorArena;

public class Dice(int faces) : IDice
{
    private readonly Random rnd = new();

    public int Roll() => rnd.Next(1, faces + 1);
}
