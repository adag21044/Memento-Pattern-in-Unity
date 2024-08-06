using System;
using UnityEngine;
using Newtonsoft.Json;

public class Originator : MonoBehaviour
{
    public Vector3 PlayerPosition { get; set; }
    public int PlayerScore { get; set; }
    public int PlayerHealth { get; set; }

    private GameObject _player;

    private void Start()
    {
        _player = gameObject;
        PlayerPosition = _player.transform.position;
    }

    private void Update()
    {
        // Update GameObject position when PlayerPosition changes
        _player.transform.position = PlayerPosition;
    }

    public IMemento SaveStateToMemento()
    {
        var gameState = new GameState
        {
            PlayerPosition = new SerializableVector3(PlayerPosition),
            PlayerScore = PlayerScore,
            PlayerHealth = PlayerHealth
        };
        string stateJson = JsonConvert.SerializeObject(gameState);
        return new Memento(stateJson);
    }

    public void GetStateFromMemento(IMemento memento)
    {
        GameState gameState = JsonConvert.DeserializeObject<GameState>(memento.GameStateJson);
        PlayerPosition = gameState.PlayerPosition.ToVector3();
        PlayerScore = gameState.PlayerScore;
        PlayerHealth = gameState.PlayerHealth;

        // Update GameObject position with restored state
        _player.transform.position = PlayerPosition;
    }

    [Serializable]
    private class GameState
    {
        public SerializableVector3 PlayerPosition;
        public int PlayerScore;
        public int PlayerHealth;
    }
}