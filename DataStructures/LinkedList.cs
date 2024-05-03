using System.Collections;

namespace DataStructures;

public class LinkedList<T> : IEnumerable<T>
{
    private INode _head;

    public LinkedList() =>
        _head = new EmptyListHead();

    public void Prepend(T value) => 
        _head = _head.Prepend(value);

    public void Add(T value) =>
        _head = _head.Add(value);

    public IEnumerator<T> GetEnumerator() => _head.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private interface INode : IEnumerable<T>
    {
        INode Prepend(T value);
        public INode Add(T value);
        IEnumerable<T> Enumerate();
    }

    private interface IValueNode : INode
    {
        T Value { get; }
    }

    private record EmptyListHead : INode
    {
        public INode Prepend(T value) => new IsolatedNode(value);
        public INode Add(T value) => new IsolatedNode(value);
        public IEnumerable<T> Enumerate() => new List<T>();
        public IEnumerator<T> GetEnumerator() => 
            new List<T>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }

    private record IsolatedNode(T Value) : IValueNode
    {
        public INode Prepend(T value) => 
            new LinkedNode(value, new IsolatedNode(Value));

        public INode Add(T value) => 
            new LinkedNode(Value, new IsolatedNode(value));
        
        public IEnumerable<T> Enumerate() => new[] { Value };

        public IEnumerator<T> GetEnumerator() => 
            new List<T> { Value }.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }

    private class LinkedNode : IValueNode
    {
        public T Value { get; }
        private INode Next { get; set; }
        
        public LinkedNode(T value, INode next)
        {
            Value = value;
            Next = next;
        }

        public INode Prepend(T value) => 
            new LinkedNode(value, this);

        public INode Add(T value)
        {
            var current = this;

            while (current.Next is LinkedNode next) 
                current = next;

            var lastNode = (IsolatedNode) current.Next;
            
            current.Next = new LinkedNode(lastNode.Value, new IsolatedNode(value));
            
            return this;
        }

        public IEnumerator<T> GetEnumerator() => Enumerate().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public IEnumerable<T> Enumerate() => new[] { Value }.Concat(Next.Enumerate());
    }
}