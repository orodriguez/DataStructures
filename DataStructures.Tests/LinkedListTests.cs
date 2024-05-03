namespace DataStructures.Tests;

public class LinkedListTests
{
    [Fact]
    public void Enumerate_Empty()
    {
        var list = new LinkedList<int>();
        Assert.Empty(list);
    }

    [Fact]
    public void Enumerate_1Element()
    {
        var list = new LinkedList<int> { 1 };

        var value = Assert.Single(list);
        Assert.Equal(1, value);
    }

    [Fact]
    public void Enumerate_2Elements()
    {
        var list = new LinkedList<int> { 1, 2 };

        Assert.Equal(new[] { 1, 2 }, list);
    }

    [Fact]
    public void Enumerate_ManyElements()
    {
        var list = new LinkedList<int> { 1, 2, 3, 4 };

        Assert.Equal(new[] { 1, 2, 3, 4 }, list);
    }

    [Fact]
    public void Enumerate_AfterPrepend()
    {
        var list = new LinkedList<string> { "B" };

        list.Prepend("A");

        Assert.Equal(new[] { "A", "B" }, list);
    }

    [Fact]
    public void Enumerate_AfterManyPrepend()
    {
        var list = new LinkedList<char> { 'D' };

        list.Prepend('C');
        list.Prepend('B');
        list.Prepend('A');

        Assert.Equal(new[] { 'A', 'B', 'C', 'D' }, list);
    }
}