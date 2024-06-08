using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIGuessController : MonoBehaviour
{
    [SerializeField] private PieceData _selectedPiece;
    [SerializeField] private List<int> _guess;
    [SerializeField] private UIGuessSlot _uiGuessSlotPrefab;
    [SerializeField] private UIGuessPiece _uiGuessPiecePrefab;
    [SerializeField] private List<UIGuessSlot> _slots;
    [SerializeField] private List<UIGuessPiece> _options;

    [SerializeField] private Transform _panelGuess;
    [SerializeField] private Transform _panelOptions;
    [SerializeField] private Button _outsideButton;
    [SerializeField] private UIGuessButton _uiGuessButton;

    private bool _allowRepetition;

    public List<int> Guess { get => _guess; }

    public void OnOptionClick(UIGuessPiece uiGuessPiece, PieceData selectedPiece)
    {
        if(!_selectedPiece.IsEmpty())
            GetSelectedPiece().UnShrink();

        _selectedPiece = selectedPiece;
    }

    public void OnSlotClick(UIGuessSlot uiGuessSlot, int index)
    {

        //if slot is free
        if (!_selectedPiece.IsEmpty())
        {
            uiGuessSlot.FillWithOption(_selectedPiece);
            _guess[index] = _selectedPiece.value;
            GetSelectedPiece().UnShrink();
            _selectedPiece.Clear();
        }
        //if slot is occuppied
        else
        {
            //Clear only if it is already filled
            if(_guess[index] != -1) {
                _guess[index] = -1;
                uiGuessSlot.Clear();
            }
        }

        //Show Guess Button if it is complete
        if (!_guess.Contains(-1))
            _uiGuessButton.Show();
        else
            _uiGuessButton.Hide();
    }

    // Called from GameController
    public async void ClearGuessController()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            await Task.Delay(100);
            _guess[i] = -1;
            _slots[i].Clear();
        }
    }

    public void OnOutsideClick()
    {
        GetSelectedPiece()?.UnShrink();
        _selectedPiece.Clear();
    }

    private UIGuessPiece GetSelectedPiece()
    {
        if(_selectedPiece.IsEmpty())
            return null;

        return _options.FirstOrDefault(x => x.Value == _selectedPiece.value);
    }
    
    public void CreateSlots(GameController gameController, int size, List<Sprite> options) {
        CreateGuessSlots(size);
        CreateGuessPieces(options);
        _outsideButton.onClick.AddListener(OnOutsideClick);
        //_uiGuessButton.Button.onClick.AddListener(OnGuessClick);
    }

    private void CreateGuessSlots(int size)
    {
        _slots = new List<UIGuessSlot>();
        _guess = new List<int>();
        for (int i = 0; i < size; i++)
        {
            _slots.Add(Instantiate(_uiGuessSlotPrefab).Setup(this, i, _panelGuess));
            _guess.Add(-1);
        }
    }

    private void CreateGuessPieces(List<Sprite> options)
    {
        _options = new List<UIGuessPiece>();
        for (int i = 0; i < options.Count; i++)
        {
            _options.Add(Instantiate(_uiGuessPiecePrefab).Setup(this, i, options[i], _panelOptions));
        }
    }
    
}
