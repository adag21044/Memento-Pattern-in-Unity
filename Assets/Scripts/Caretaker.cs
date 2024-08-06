using System.Collections.Generic;

public class Caretaker : ICaretaker
{
    private readonly List<IMemento> _mementoList = new List<IMemento>();

    public void Add(IMemento state)
    {
        _mementoList.Add(state);
    }

    public IMemento Get(int index)
    {
        return _mementoList[index];
    }

    public int Count => _mementoList.Count;

    public bool HasPreviousState(int currentIndex)
    {
        return currentIndex > 0;
    }

    public bool HasNextState(int currentIndex)
    {
        return currentIndex < _mementoList.Count - 1;
    }

    public void Trim(int startIndex)
    {
        if (startIndex < _mementoList.Count)
        {
            _mementoList.RemoveRange(startIndex, _mementoList.Count - startIndex);
        }
    }
}