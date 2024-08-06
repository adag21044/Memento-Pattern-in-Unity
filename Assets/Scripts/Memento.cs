//This class will store the game state.
using System;
using Newtonsoft.Json;

[Serializable]
public class Memento : IMemento
{
    public string GameStateJson { get; private set; }

    public Memento(string gameStateJson)
    {
        GameStateJson = gameStateJson;
    }
}