using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGuessPiece : MonoBehaviour, IPointerDownHandler
{
    public enum PieceState { UNSET, SET }

    [SerializeField] private int _value;
    [SerializeField] private PieceState _state;

    [Space]
    [SerializeField] private Transform _origin;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _image;
    [SerializeField] private AudioSource _audioSource;
    private Vector3 _originPosition;
    private UIGuessController _uiGuessController;

    public int Value { get => _value; }
    public PieceState State { get => _state; set => _state = value; }

    public UIGuessPiece Setup(UIGuessController uiGuessController, int value, Sprite sprite, Transform parent)
    {
        _uiGuessController = uiGuessController;
        _value = value;
        _image.sprite = sprite;
        transform.SetParent(parent);
        _originPosition = _rectTransform.anchoredPosition;
        _rectTransform.localScale = Vector3.one;
        return this;
    }

    public void Reset() {
        transform.SetParent(_origin);
        _rectTransform.anchoredPosition = _originPosition;
        _state = PieceState.UNSET;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PieceData pieceData = new PieceData();
        pieceData.value = _value;
        pieceData.sprite = _image.sprite;
        _uiGuessController.OnOptionClick(this, pieceData);
        _rectTransform.DOScale(new Vector3(.7f,.7f,.7f), 1f).SetEase(Ease.OutElastic);
        _audioSource.pitch = Random.Range(0.52f, 1.7f);
        _audioSource.Play();
    }

    public void UnShrink()
    {
        _rectTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
    }
}
