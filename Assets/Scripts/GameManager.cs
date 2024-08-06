using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Originator _originator;
    private ICaretaker _caretaker;
    private int _currentStateIndex;

    // UI references
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        _originator = FindObjectOfType<Originator>();
        _caretaker = new Caretaker();
        _currentStateIndex = -1;

        // Initial state
        InitializeGameState();

        // Initialize UI
        UpdateUI();
    }

    private void Update()
    {
        HandleInput();
    }

    private void InitializeGameState()
    {
        _originator.PlayerPosition = new Vector3(0, 0, 0);
        _originator.PlayerScore = 0;
        _originator.PlayerHealth = 100;

        // Save the initial state
        SaveGameState();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpdateGameState(new Vector3(1, 0, 0), 10, -5);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UndoGameState();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RedoGameState();
        }
    }

    private void UpdateGameState(Vector3 positionDelta, int scoreDelta, int healthDelta)
    {
        _originator.PlayerPosition += positionDelta;
        _originator.PlayerScore += scoreDelta;
        _originator.PlayerHealth += healthDelta;
        Debug.Log($"Updated State - Position: {_originator.PlayerPosition}, Score: {_originator.PlayerScore}, Health: {_originator.PlayerHealth}");
        SaveGameState();
        UpdateUI();
    }

    private void SaveGameState()
    {
        IMemento memento = _originator.SaveStateToMemento();
        // Remove all states after the current state index for proper redo functionality
        if (_currentStateIndex < _caretaker.Count - 1)
        {
            _caretaker.Trim(_currentStateIndex + 1);
        }
        _caretaker.Add(memento);
        _currentStateIndex = _caretaker.Count - 1;
        Debug.Log($"Game Saved - CurrentStateIndex: {_currentStateIndex}");
    }

    private void UndoGameState()
    {
        if (!_caretaker.HasPreviousState(_currentStateIndex))
        {
            Debug.Log("Undo: No previous state to undo.");
            return;
        }
        _currentStateIndex--;
        IMemento memento = _caretaker.Get(_currentStateIndex);
        _originator.GetStateFromMemento(memento);
        Debug.Log($"Undo: {_currentStateIndex} - Position: {_originator.PlayerPosition}, Score: {_originator.PlayerScore}, Health: {_originator.PlayerHealth}");
        UpdateUI();
    }

    private void RedoGameState()
    {
        if (!_caretaker.HasNextState(_currentStateIndex))
        {
            Debug.Log("Redo: No next state to redo.");
            return;
        }
        _currentStateIndex++;
        IMemento memento = _caretaker.Get(_currentStateIndex);
        _originator.GetStateFromMemento(memento);
        Debug.Log($"Redo: {_currentStateIndex} - Position: {_originator.PlayerPosition}, Score: {_originator.PlayerScore}, Health: {_originator.PlayerHealth}");
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + _originator.PlayerScore;
        healthText.text = "Health: " + _originator.PlayerHealth;
    }
}
