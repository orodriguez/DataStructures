namespace DataStructures.Tests;

public class LinkedListTests
{
    [Fact]
    public void Empty_Enumerate()
    {
        var list = LinkedList.Empty<int>();
        Assert.Empty(list);
    }

    [Fact]
    public void Add1_Enumerate()
    {
        var list = LinkedList.From(1);

        var value = Assert.Single(list);
        Assert.Equal(1, value);
    }

    [Fact]
    public void Add2_Enumerate()
    {
        var list = LinkedList.From(1, 2);

        Assert.Equal(new[] { 1, 2 }, list);
    }

    [Fact]
    public void Add4_Enumerate()
    {
        var list = LinkedList.From(1, 2, 3, 4);

        Assert.Equal(new[] { 1, 2, 3, 4 }, list);
    }

    [Fact]
    public void Add1_Prepend1_Enumerate()
    {
        var list = LinkedList.From("B").Prepend("A");

        Assert.Equal(new[] { "A", "B" }, list);
    }

    [Fact]
    public void Add1_Prepend3_Enumerate()
    {
        var list = LinkedList.From('D')
            .Prepend('C')
            .Prepend('B')
            .Prepend('A');

        Assert.Equal(new[] { 'A', 'B', 'C', 'D' }, list);
    }

    [Fact]
    public void Add3_Prepend3_Enumerate()
    {
        var list = LinkedList.From('D', 'E', 'F')
            .Prepend('C')
            .Prepend('B')
            .Prepend('A');

        Assert.Equal(new[] { 'A', 'B', 'C', 'D', 'E', 'F' }, list);
    }

    [Fact]
    public void Add1_Remove_Enumerate()
    {
        var list = LinkedList.From('X')
            .Remove('X');

        Assert.Empty(list);
    }

    [Fact]
    public void Add2_RemoveFirst_Enumerate()
    {
        var list = LinkedList.From('X', 'A')
            .Remove('X');

        Assert.Equal(new[] { 'A' }, list);
    }

    [Fact]
    public void Add2_RemoveSecond_Enumerate()
    {
        var list = LinkedList.From('A', 'X')
            .Remove('X');

        Assert.Equal(new[] { 'A' }, list);
    }

    [Fact]
    public void Add3_RemoveFirst_Enumerate()
    {
        var list = LinkedList.From('X', 'A', 'B')
            .Remove('X');

        Assert.Equal(new[] { 'A', 'B' }, list);
    }

    [Fact]
    public void Add3_RemoveSecond_Enumerate()
    {
        var list = LinkedList.From('A', 'X', 'B')
            .Remove('X');

        Assert.Equal(new[] { 'A', 'B' }, list);
    }

    [Fact]
    public void Add3_RemoveLast_Enumerate()
    {
        var list = LinkedList.From('A', 'B', 'X')
            .Remove('X');

        Assert.Equal(new[] { 'A', 'B' }, list);
    }

    [Fact]
    public void Add5_RemoveFirst_Enumerate()
    {
        var list = LinkedList.From('X', 'A', 'B', 'C', 'D')
            .Remove('X');

        Assert.Equal(new[] { 'A', 'B', 'C', 'D' }, list);
    }

    [Fact]
    public void Add5_RemoveMiddle_Enumerate()
    {
        var list = LinkedList.From('A', 'B', 'X', 'C', 'D')
            .Remove('X');

        Assert.Equal(new[] { 'A', 'B', 'C', 'D' }, list);
    }

    [Fact]
    public void Add5_RemoveLast_Enumerate()
    {
        var list = LinkedList.From('A', 'B', 'C', 'D', 'X')
            .Remove('X');

        Assert.Equal(new[] { 'A', 'B', 'C', 'D' }, list);
    }

    [Fact]
    public void Empty_RemoveNonExisting_Enumerate()
    {
        var list = LinkedList.Empty<char>().Remove('X');

        Assert.Empty(list);
    }

    [Fact]
    public void Add1_RemoveNonExisting_Enumerate()
    {
        var list = LinkedList.From('A')
            .Remove('X');

        Assert.Single(list);
    }

    [Fact]
    public void Add2_RemoveNonExisting_Enumerate()
    {
        var list = LinkedList.From('A', 'B')
            .Remove('X');

        Assert.Equal(new[] { 'A', 'B' }, list);
    }

    [Fact]
    public void Add4_RemoveNonExisting_Enumerate()
    {
        var list = LinkedList.From('A', 'B', 'C', 'D')
            .Remove('X');

        Assert.Equal(new[] { 'A', 'B', 'C', 'D' }, list);
    }

    [Fact]
    public void Add4_Remove4_Enumerate()
    {
        var list = LinkedList.From('A', 'B', 'C', 'D')
            .Remove('B')
            .Remove('A')
            .Remove('C')
            .Remove('D');

        Assert.Empty(list);
    }
}