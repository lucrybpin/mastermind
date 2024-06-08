using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct PieceData
{
    public int value;
    public Sprite sprite;

    public bool IsEmpty()
    {
        return value == -1;
    }

    public void Clear()
    {
        value = -1;
        sprite = null;
    }
}

public class GameController : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private UIGuessController _uiGuessController;
    [SerializeField] private UIResultsController _uiResultsController;
    [SerializeField] private GameService _gameService;
    [SerializeField] private Button _guessButton;

    void Start()
    {
        List<int> possibleValues = new List<int>() { 0, 1, 2, 3, 4, 5 };
        _gameService = new GameService();
        _gameService.NewGame(possibleValues, _gameData.SecretSize, false);
        _uiResultsController.CreateRows(_gameData.NumberOfGuess, _gameData.SecretSize, _gameData);
        _uiGuessController.CreateSlots(this, _gameData.SecretSize, _gameData.Options);
        _guessButton.onClick.AddListener(OnGuessClick);
    }

    // Button callbacks
    public void OnGuessClick()
    {
        List<int> guess = _uiGuessController.Guess;
        _uiGuessController.ClearGuessController();

        int blackPegs, whitePegs;
        (blackPegs, whitePegs) = _gameService.Guess(guess);
        _uiResultsController.FillResult(guess, blackPegs, whitePegs); //WIP
    }
    
    // Support methods

}
