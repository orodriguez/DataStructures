namespace DataStructures;

public interface ILinkedList<T> : IEnumerable<T>
{
    void Prepend(T value);
    void Add(T value);
    bool Remove(T value);
}