using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGuessSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int _index;
    [SerializeField] private int _valueFilled;
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _imageRectTransform;
    [SerializeField] private AudioSource _audioSource;
    private UIGuessController _uiGuessController;

    public UIGuessSlot Setup(UIGuessController uIGuessController, int index, Transform parent)
    {
        _uiGuessController = uIGuessController;
        _index = index;
        transform.SetParent(parent);
        Clear();
        return this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _uiGuessController.OnSlotClick(this, _index);
    }

    public bool IsEmpty()
    {
        return _valueFilled == -1;
    }

    public void FillWithOption(PieceData pieceData)
    {
        _valueFilled = pieceData.value;
        _image.sprite = pieceData.sprite;
        _image.enabled = true;
        _imageRectTransform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
        _image.rectTransform.DOPunchRotation(new Vector3(0f,0f,10f), 1f, 7).OnComplete( () => {
            _image.rectTransform.rotation = Quaternion.identity;
        });
    }

    public void Clear()
    {
        _imageRectTransform.DOScale(Vector3.zero, .12f).OnComplete(() => {
            _valueFilled = -1;
            _image.sprite = null;
            _image.enabled = false;
            _imageRectTransform.localScale = new Vector3(.7f, .7f, .7f);
        });
        _audioSource.Play();
    }
}
