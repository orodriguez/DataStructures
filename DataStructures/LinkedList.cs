using System.Collections;

namespace DataStructures;

public class LinkedList<T> : IEnumerable<T> where T : notnull
{
    private INode _head;

    public LinkedList() =>
        _head = new EmptyNode();

    public IEnumerator<T> GetEnumerator() =>
        _head.EnumerateValues().GetEnumerator();

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

    private interface INode : IEnumerable<INode>
    {
        bool ValueEquals(T value);
        INode Prepend(T value);
        public INode Add(T value);
        (INode Node, bool Removed) Remove(T value);
        IEnumerable<T> EnumerateValues();
        IEnumerable<INode> Enumerate();
        INode Remove(LinkedNode previous);
        INode RemoveNext();
    }

    private record EmptyNode : INode
    {
        public bool ValueEquals(T value) => false;

        public INode Prepend(T value) => new IsolatedNode(value);

        public INode Add(T value) => new IsolatedNode(value);

        public (INode Node, bool Removed) Remove(T value) => (this, false);

        public IEnumerable<T> EnumerateValues() => Array.Empty<T>();

        public INode Remove(LinkedNode previous) => this;

        public INode RemoveNext() => this;

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public IEnumerator<INode> GetEnumerator() =>
            new List<INode>().GetEnumerator();

        public IEnumerable<INode> Enumerate() => new List<INode>();
    }

    private record IsolatedNode(T Value) : INode
    {
        public bool ValueEquals(T value) => Value.Equals(value);

        public INode Prepend(T value) =>
            new LinkedNode(value, this);

        public INode Add(T value) =>
            new LinkedNode(Value, new IsolatedNode(value));

        public (INode Node, bool Removed) Remove(T value) =>
            Value.Equals(value) ? (new EmptyNode(), true) : (this, false);

        public IEnumerable<T> EnumerateValues() => new[] { Value };

        public INode Remove(LinkedNode previous) =>
            new IsolatedNode(previous.Value);

        public INode RemoveNext() => this;

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public IEnumerator<INode> GetEnumerator() =>
            new List<INode> { this }.GetEnumerator();

        public IEnumerable<INode> Enumerate() => new[] { this };
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

        public INode Prepend(T value) => new LinkedNode(value, this);

        public INode Add(T value)
        {
            this.OfType<LinkedNode>()
                .Last()
                .AddToNext(value);

            return this;
        }

        public (INode Node, bool Removed) Remove(T value)
        {
            if (ValueEquals(value))
                return (Next, true);

            if (Next.ValueEquals(value))
                return (Next.Remove(this), true);

            foreach (var node in this.OfType<LinkedNode>())
            {
                var target = node.Skip(2).FirstOrDefault();

                if (target == null)
                    return (this, false);

                if (!target.ValueEquals(value)) continue;

                node.RemoveNextFromNext();
                return (this, true);
            }

            return (this, false);
        }

        public IEnumerable<T> EnumerateValues()
        {
            INode current = this;
            while (true)
            {
                if (current is LinkedNode linkedNode)
                {
                    yield return linkedNode.Value;
                    current = linkedNode.Next;
                }

                if (current is IsolatedNode isolated)
                {
                    yield return isolated.Value;
                    break;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<INode> GetEnumerator() => Enumerate().GetEnumerator();

        public IEnumerable<INode> Enumerate() => new[] { this }.Concat(Next.Enumerate());

        private void RemoveNextFromNext() => Next = Next.RemoveNext();

        private void AddToNext(T value) => Next = Next.Add(value);

        private bool NextValueEquals(T value) => Next.ValueEquals(value);

        public override string ToString() =>
            $"{nameof(LinkedNode)}({Value},{Next})";
    }
}