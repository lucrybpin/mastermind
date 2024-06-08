using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIResultSlot : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _image;
    //TODO: Clear

    public UIResultSlot Setup()
    {
        _rectTransform.localScale = Vector3.zero;
        return this;
    }

    public UIResultSlot Spawn()
    {
        _rectTransform.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutElastic);
        return this;
    }

    public void Fill(Sprite sprite)
    {
        Debug.Log($"Fill Sprite: {sprite}");
        _image.gameObject.SetActive(true);
        _image.sprite = sprite;

        _rectTransform.localScale = Vector3.zero;
        _rectTransform.DOScale(Vector3.one, 2f).SetEase(Ease.OutElastic);
        _image.rectTransform.DOPunchRotation(new Vector3(0f,0f,10f), 1f, 7).OnComplete( () => {
            _image.rectTransform.rotation = Quaternion.identity;
        });
       ;
    }

}
