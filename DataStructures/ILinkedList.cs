namespace DataStructures;

public interface ILinkedList<T> : IEnumerable<T>
{
    ILinkedList<T> Prepend(T value);
    ILinkedList<T> Add(T value);
    ILinkedList<T> Remove(T value);
}