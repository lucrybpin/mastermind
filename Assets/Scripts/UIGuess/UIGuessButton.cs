using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIGuessButton : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _button;
    Sequence _breathSequence;

    public Ease easeType;

    public Button Button { get => _button; }

    void Start()
    {
        _breathSequence = DOTween.Sequence();
        _canvasGroup.alpha = 0;
        // Inicializa a sequência, mas não a começa ainda
        _breathSequence = DOTween.Sequence()
            .Append(_rectTransform.DOScale(new Vector3(.84f, .84f, .84f), .57f))
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo)
            .Pause() // Pausa a sequência para iniciar apenas quando necessário
            .SetAutoKill(false); // Evita que a sequência seja destruída ao ser pausada/completada
    }

    [ContextMenu("Show")]
    public void Show()
    {
        _breathSequence.Restart();
        _canvasGroup.interactable = true;
        _canvasGroup.DOFade(1f, .7f);
    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        _breathSequence.Pause();
        _canvasGroup.interactable = false;
        _canvasGroup.DOFade(0f, .57f);
    }

}
