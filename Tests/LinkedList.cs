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

    private interface IValueNode : INode
    {
        T Value { get; }
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

    private record IsolatedNode(T Value) : IValueNode
    {
        public INode Add(T newValue) => 
            new LinkedNode(Value, new IsolatedNode(newValue));
        public IEnumerable<T> Enumerate() => new[] { Value };
        public bool IsBeforeLast => false;
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

        public INode Add(T newValue)
        {
            var current = this;

            while (current.Next is LinkedNode next) 
                current = next;

            var lastNode = (IsolatedNode) current.Next;
            
            current.Next = new LinkedNode(lastNode.Value, new IsolatedNode(newValue));
            
            return this;
        }

        public IEnumerator<T> GetEnumerator() => 
            Enumerate().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public IEnumerable<T> Enumerate() => 
            new[] { Value }.Concat(Next.Enumerate());
    }
}