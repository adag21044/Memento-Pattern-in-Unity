public interface ICaretaker
{
    void Add(IMemento state);
    IMemento Get(int index);
    int Count { get; }
    bool HasPreviousState(int currentIndex);
    bool HasNextState(int currentIndex);
    void Trim(int startIndex);
}