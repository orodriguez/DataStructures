namespace Tests;

public class LinkedListTests
{
    [Fact]
    public void Empty()
    {
        var list = new LinkedList<int>();
        Assert.Empty(list);
    }

    [Fact]
    public void OneElement_Enumerate()
    {
        var list = new LinkedList<int> { 1 };

        var value = Assert.Single(list);
        Assert.Equal(1, value);
    }

    [Fact]
    public void TwoElements_Enumerate()
    {
        var list = new LinkedList<int> { 1, 2 };

        Assert.Equal(new[] { 1, 2 }, list);
    }
}