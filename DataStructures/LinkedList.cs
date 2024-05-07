using System.Collections;

namespace DataStructures;

public class LinkedList<T> : IEnumerable<T> where T : notnull
{
    private INode _head;

    public LinkedList() =>
        _head = new EmptyListHead();

    public IEnumerator<T> GetEnumerator() => _head.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    public void Prepend(T value) =>
        _head = _head.Prepend(value);

    public void Add(T value) =>
        _head = _head.Add(value);

    public bool Remove(T value)
    {
        (_head, var removed) = _head.Remove(value);
        return removed;
    }

    private interface INode : IEnumerable<T>
    {
        bool ValueEquals(T value);
        INode Prepend(T value);
        public INode Add(T value);
        (INode Node, bool Removed) Remove(T value);
        IEnumerable<T> Enumerate();
        INode Remove(LinkedNode previous);
        INode RemoveNext();
    }

    private record EmptyListHead : INode
    {
        public bool ValueEquals(T value) => false;

        public INode Prepend(T value) => new IsolatedNode(value);

        public INode Add(T value) => new IsolatedNode(value);

        public (INode Node, bool Removed) Remove(T value) => (this, false);

        public IEnumerable<T> Enumerate() => new List<T>();

        public INode Remove(LinkedNode previous) => this;

        public INode RemoveNext() => this;

        public IEnumerator<T> GetEnumerator() =>
            new List<T>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    private record IsolatedNode(T Value) : INode
    {
        public bool IsLast => true;
        public bool ValueEquals(T value) => Value.Equals(value);

        public INode Prepend(T value) =>
            new LinkedNode(value, new IsolatedNode(Value));

        public INode Add(T value) =>
            new LinkedNode(Value, new IsolatedNode(value));

        public (INode Node, bool Removed) Remove(T value) =>
            Value.Equals(value) ? (new EmptyListHead(), true) : (this, false);

        public IEnumerable<T> Enumerate() => new[] { Value };

        public INode Remove(LinkedNode previous) =>
            new IsolatedNode(previous.Value);

        public INode RemoveNext() => this;

        public IEnumerator<T> GetEnumerator() =>
            new List<T> { Value }.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    private class LinkedNode : INode
    {
        public LinkedNode(T value, INode next)
        {
            Value = value;
            Next = next;
        }

        private INode Next { get; set; }
        public T Value { get; }

        public INode Remove(LinkedNode previous) =>
            new LinkedNode(previous.Value, Next);

        public INode RemoveNext() =>
            Next.Remove(this);

        public bool ValueEquals(T value) => Value.Equals(value);

        public INode Prepend(T value) =>
            new LinkedNode(value, this);

        public INode Add(T value)
        {
            var current = this;

            while (current.Next is LinkedNode next)
                current = next;
            
            current.Next = current.Next.Add(value);

            return this;
        }

        public (INode Node, bool Removed) Remove(T value)
        {
            if (ValueEquals(value))
                return (Next, true);

            if (Next.ValueEquals(value))
                return (Next.Remove(this), true);

            var previous = this;
            var current = Next;

            while (current is LinkedNode linkedNode)
            {
                if (linkedNode.NextValueEquals(value))
                {
                    previous.Next = current.RemoveNext();
                    return (this, true);
                }

                current = linkedNode.Next;
                previous = linkedNode;
            }

            return (this, false);
        }

        public IEnumerator<T> GetEnumerator() => Enumerate().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> Enumerate() => new[] { Value }.Concat(Next.Enumerate());

        private bool NextValueEquals(T value) => Next.ValueEquals(value);

        public override string ToString() =>
            $"{nameof(LinkedNode)}({Value},{Next})";
    }
}