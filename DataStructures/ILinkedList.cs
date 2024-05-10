namespace DataStructures;

public interface ILinkedList<T> : IEnumerable<T>
{
    ILinkedList<T> Prepend(T value);
    void Add(T value);
    bool Remove(T value);
}