using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum UIResultPegSlotFill
{
    Empty,
    Black,
    White
}

public class UIResultPegSlot : MonoBehaviour
{

    [SerializeField] private UIResultPegSlotFill _fillCollor;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _image;

    public UIResultPegSlot Setup()
    {
        _rectTransform.localScale = Vector3.zero;
        return this;
    }

    public void Fill(UIResultPegSlotFill color)
    {
        switch (color)
        {
            case UIResultPegSlotFill.Black:
                _image.color = Color.black;
                break;

            case UIResultPegSlotFill.White:
                _image.color = Color.white;
                break;
        }
    }

    public UIResultPegSlot Spawn()
    {
        _rectTransform.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutElastic);
        return this;
    }
}
