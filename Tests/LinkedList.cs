using System.Collections;

namespace Tests;

public class LinkedList<T> : IEnumerable<T>
{
    private INode _head;

    public LinkedList() =>
        _head = new EmptyListHead();

    public IEnumerator<T> GetEnumerator() => _head.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    public void Add(T value) =>
        _head = _head.Add(value);
    
    private interface INode : IEnumerable<T>
    {
        public INode Add(T newValue);
        IEnumerable<T> Enumerate();
    }

    private record EmptyListHead : INode
    {
        public INode Add(T newValue) => new IsolatedNode(newValue);
        public IEnumerable<T> Enumerate() => new List<T>();
        public IEnumerator<T> GetEnumerator() => 
            new List<T>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }

    private record IsolatedNode(T Value) : INode
    {
        public INode Add(T newValue)
        {
            var firstNode = new FirstNode(Value, Next: new IsolatedNode(newValue));
            return firstNode with { Next = new LastNode(newValue, Previous: firstNode)};
        }

        public IEnumerable<T> Enumerate() => new[] { Value };

        public IEnumerator<T> GetEnumerator() => 
            new List<T> { Value }.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }
    
    private record FirstNode(T Value, INode Next) : INode
    {
        public INode Add(T newValue)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator() => 
            Enumerate().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> Enumerate() => 
            new[] { Value }.Concat(Next.Enumerate());
    }
    
    private record LastNode(T Value, FirstNode Previous) : INode
    {
        public INode Add(T newValue)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Enumerate()
        {
            yield return Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}