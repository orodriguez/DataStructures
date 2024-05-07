using System.Collections;

namespace DataStructures;

public class LinkedList<T> : IEnumerable<T> where T : notnull
{
    private INode _head;

    public LinkedList() =>
        _head = new EmptyListHead();

    public void Prepend(T value) =>
        _head = _head.Prepend(value);

    public void Add(T value) =>
        _head = _head.Add(value);

    public bool Remove(T value)
    {
        (_head, var removed) = _head.Remove(value);
        return removed;
    }

    public IEnumerator<T> GetEnumerator() => _head.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private interface INode : IEnumerable<T>
    {
        bool ValueEquals(T value);
        bool NextValueEquals(T value);
        INode Prepend(T value);
        public INode Add(T value);
        (INode Node, bool Removed) Remove(T value);
        IEnumerable<T> Enumerate();
        T Value { get; }
    }

    private record EmptyListHead : INode
    {
        public bool ValueEquals(T value) => false;

        public bool NextValueEquals(T value)
        {
            throw new NotImplementedException();
        }

        public INode Prepend(T value) => new IsolatedNode(value);

        public INode Add(T value) => new IsolatedNode(value);

        public (INode Node, bool Removed) Remove(T value) => (this, false);

        public IEnumerable<T> Enumerate() => new List<T>();
        public T Value => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator() =>
            new List<T>().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    private record IsolatedNode(T Value) : INode
    {
        public bool ValueEquals(T value) => Value.Equals(value);

        public bool NextValueEquals(T value)
        {
            throw new NotImplementedException();
        }

        public INode Prepend(T value) =>
            new LinkedNode(value, new IsolatedNode(Value));

        public INode Add(T value) =>
            new LinkedNode(Value, new IsolatedNode(value));

        public (INode Node, bool Removed) Remove(T value) =>
            Value.Equals(value) ? (new EmptyListHead(), true) : (this, false);

        public IEnumerable<T> Enumerate() => new[] { Value };

        public IEnumerator<T> GetEnumerator() =>
            new List<T> { Value }.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    private class LinkedNode : INode
    {
        public T Value { get; }
        private INode Next { get; set; }

        public LinkedNode(T value, INode next)
        {
            Value = value;
            Next = next;
        }

        public bool ValueEquals(T value) => Value.Equals(value);
        public bool NextValueEquals(T value) => Next.ValueEquals(value);

        public INode Prepend(T value) =>
            new LinkedNode(value, this);

        public INode Add(T value)
        {
            var current = this;

            while (current.Next is LinkedNode next)
                current = next;

            var lastNode = (IsolatedNode)current.Next;

            current.Next = new LinkedNode(lastNode.Value, new IsolatedNode(value));

            return this;
        }

        public (INode Node, bool Removed) Remove(T value)
        {
            if (ValueEquals(value))
                return (Next, true);

            if (Next.ValueEquals(value) && Next is IsolatedNode)
                return (new IsolatedNode(Value), true);

            if (Next.ValueEquals(value) && Next is LinkedNode linked)
                return (new LinkedNode(Value, linked.Next), true);

            var previous = this;
            var current = this.Next;

            while (true)
            {
                if (current is not LinkedNode linkedCurrent) return (this, false);
                
                if (linkedCurrent.Next.ValueEquals(value))
                {
                    if (linkedCurrent.Next is IsolatedNode)
                    {
                        previous.Next = new IsolatedNode(linkedCurrent.Value);
                        return (this, true);
                    }

                    if (linkedCurrent.Next is LinkedNode linkedNext)
                    {
                        previous.Next = new LinkedNode(linkedCurrent.Value, linkedNext.Next);
                        return (this, true);
                    }
                }

                if (current.ValueEquals(value))
                {
                    previous.Next = new LinkedNode(previous.Value, linkedCurrent.Next);
                    return (this, true);
                }

                current = linkedCurrent.Next;
                previous = linkedCurrent;
                continue;

            }
        }

        public IEnumerator<T> GetEnumerator() => Enumerate().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> Enumerate() => new[] { Value }.Concat(Next.Enumerate());

        public override string ToString() =>
            $"{nameof(LinkedNode)}({Value},{Next})";
    }
}