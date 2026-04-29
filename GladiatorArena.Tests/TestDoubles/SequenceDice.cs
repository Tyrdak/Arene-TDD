namespace GladiatorArena.Tests.TestDoubles;

public class SequenceDice(params int[] values) : IDice
{
    private int index = 0;

    public int Roll() => values[index++];
}
