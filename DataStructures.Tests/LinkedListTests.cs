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
    public void Enumerate_AfterAdd()
    {
        var list = new LinkedList<int> { 1 };

        var value = Assert.Single(list);
        Assert.Equal(1, value);
    }

    [Fact]
    public void Enumerate_AfterAdd2()
    {
        var list = new LinkedList<int> { 1, 2 };

        Assert.Equal(new[] { 1, 2 }, list);
    }

    [Fact]
    public void Enumerate_AfterAddMany()
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
    public void Enumerate_AfterPrependMany()
    {
        var list = new LinkedList<char> { 'D' };

        list.Prepend('C');
        list.Prepend('B');
        list.Prepend('A');

        Assert.Equal(new[] { 'A', 'B', 'C', 'D' }, list);
    }
    
    [Fact]
    public void Enumerate_AfterAddAndPrepend()
    {
        var list = new LinkedList<char>
        {
            'D',
            'E',
            'F'
        };

        list.Prepend('C');
        list.Prepend('B');
        list.Prepend('A');

        Assert.Equal(new[] { 'A', 'B', 'C', 'D', 'E', 'F' }, list);
    }

    [Fact]
    public void Enumerate_AfterRemove_ValueNotFound()
    {
        var list = new LinkedList<char>();
        
        Assert.False(list.Remove('A'));
        Assert.Empty(list);
    }
}