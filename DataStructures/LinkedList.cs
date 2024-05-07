using System.Collections;
using System.Linq.Expressions;

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
        bool IsLast { get; }
        INode Remove(LinkedNode previous);
        INode RemoveNext();
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
        public bool IsLast => true;

        public INode Remove(LinkedNode previous)
        {
            throw new NotImplementedException();
        }

        public INode RemoveNext()
        {
            throw new NotImplementedException();
        }

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
        public bool IsLast => true;

        public INode Remove(LinkedNode previous)
        {
            return new IsolatedNode(previous.Value);
        }

        public INode RemoveNext() => this;

        public IEnumerator<T> GetEnumerator() =>
            new List<T> { Value }.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    private class LinkedNode : INode
    {
        public T Value { get; }
        public bool IsLast => false;

        public INode Remove(LinkedNode previous) => 
            new LinkedNode(previous.Value, Next);

        public INode RemoveNext()
        {
            return Next.Remove(this);
        }

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

            if (Next.ValueEquals(value))
                return (Next.Remove(this), true);

            var previous = this;
            var current = Next;

            while (true)
            {
                if (current.IsLast) return (this, false);

                var linkedCurrent = (LinkedNode)current;
                
                if (current.NextValueEquals(value))
                {
                    if (linkedCurrent.Next is IsolatedNode)
                    {
                        previous.Next = current.RemoveNext();
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
            }
        }

        public IEnumerator<T> GetEnumerator() => Enumerate().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> Enumerate() => new[] { Value }.Concat(Next.Enumerate());

        public override string ToString() =>
            $"{nameof(LinkedNode)}({Value},{Next})";
    }
}