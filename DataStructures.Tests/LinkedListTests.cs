using System.Runtime.CompilerServices;

namespace DataStructures.Tests;

public class LinkedListTests
{
    [Fact]
    public void Empty_Enumerate()
    {
        var list = new LinkedList<int>();
        Assert.Empty(list);
    }

    [Fact]
    public void Add1_Enumerate()
    {
        var list = new LinkedList<int> { 1 };

        var value = Assert.Single(list);
        Assert.Equal(1, value);
    }

    [Fact]
    public void Add2_Enumerate()
    {
        var list = new LinkedList<int> { 1, 2 };

        Assert.Equal(new[] { 1, 2 }, list);
    }

    [Fact]
    public void Add4_Enumerate()
    {
        var list = new LinkedList<int> { 1, 2, 3, 4 };

        Assert.Equal(new[] { 1, 2, 3, 4 }, list);
    }

    [Fact]
    public void Add1_Prepend1_Enumerate()
    {
        var list = new LinkedList<string> { "B" };

        list.Prepend("A");

        Assert.Equal(new[] { "A", "B" }, list);
    }

    [Fact]
    public void Add1_Prepend3_Enumerate()
    {
        var list = new LinkedList<char> { 'D' };

        list.Prepend('C');
        list.Prepend('B');
        list.Prepend('A');

        Assert.Equal(new[] { 'A', 'B', 'C', 'D' }, list);
    }

    [Fact]
    public void Add3_Prepend3_Enumerate()
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
    public void Add1_Remove_Enumerate()
    {
        var list = new LinkedList<char> { 'A' };

        Assert.True(list.Remove('A'));
        Assert.Empty(list);
    }

    [Fact]
    public void Add2_RemoveFirst_Enumerate()
    {
        var list = new LinkedList<char> { 'A', 'B' };

        Assert.True(list.Remove('A'));
        Assert.Equal(new[] { 'B' }, list);
    }

    [Fact]
    public void Add2_RemoveSecond_Enumerate()
    {
        var list = new LinkedList<char> { 'A', 'B' };

        Assert.True(list.Remove('B'));
        Assert.Equal(new[] { 'A' }, list);
    }
    
    [Fact]
    public void Add3_RemoveFirst_Enumerate()
    {
        var list = new LinkedList<char> { 'A', 'B', 'C' };

        Assert.True(list.Remove('A'));
        Assert.Equal(new[] { 'B', 'C' }, list);
    }
    
    [Fact]
    public void Add3_RemoveSecond_Enumerate()
    {
        var list = new LinkedList<char> { 'A', 'B', 'C' };

        Assert.True(list.Remove('B'));
        Assert.Equal(new[] { 'A', 'C' }, list);
    }

    [Fact]
    public void Empty_RemoveNonExisting_Enumerate()
    {
        var list = new LinkedList<char>();

        Assert.False(list.Remove('A'));
        Assert.Empty(list);
    }

    [Fact]
    public void Add1_RemoveNonExisting_Enumerate()
    {
        var list = new LinkedList<char> { 'A' };

        Assert.False(list.Remove('B'));
        Assert.Single(list);
    }

    [Fact]
    public void Add2_RemoveNonExisting_Enumerate()
    {
        var list = new LinkedList<char> { 'A', 'B' };

        Assert.False(list.Remove('C'));
        Assert.Equal(new[] { 'A', 'B' }, list);
    }

    [Fact]
    public void Add4_RemoveNonExisting_Enumerate()
    {
        var list = new LinkedList<char> { 'A', 'B', 'C', 'D' };

        Assert.False(list.Remove('E'));
        Assert.Equal(new[] { 'A', 'B', 'C', 'D' }, list);
    }
}