# Memento Pattern in Unity

## Overview

This Unity project demonstrates the **Memento Design Pattern**. The Memento pattern is used to capture and restore the state of an object without exposing its internal structure. This is a useful pattern for implementing undo/redo functionality in applications.

In this example, the project manages the state of a game character using the Memento pattern. The project includes:

- **Caretaker**: Manages the storage and retrieval of Mementos.
- **Originator**: Creates and restores game states.
- **Memento**: Stores the state of the game.
- **GameManager**: Handles game state changes and interacts with the UI.

## Features

- **State Saving**: Saves the current state of the game character.
- **Undo/Redo Functionality**: Allows reverting to previous states or reapplying undone states.
- **UI Update**: Displays the character's score and health using TextMeshPro.

## Components

### Caretaker
The `Caretaker` class is responsible for storing and managing the list of Mementos. It provides methods to add, retrieve, and manage the history of states.

### Originator
The `Originator` class represents the game character and provides methods to save and restore its state. It interacts with the Memento class to manage game state.

### Memento
The `Memento` class holds the state of the game character in JSON format. This class is used to save and restore states.

### GameManager
The `GameManager` class manages user input and game state changes. It handles saving, undoing, and redoing game states and updates the UI.
