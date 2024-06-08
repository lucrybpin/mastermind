using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIResultRow : MonoBehaviour
{
    [SerializeField] List<UIResultSlot> _answerSlots;
    [SerializeField] List<UIResultPegSlot> _pegSlots;
    [SerializeField] UIResultSlot _answerSlotPrefab;
    [SerializeField] UIResultPegSlot _pegSlotPrefab;
    [SerializeField] private Transform _answerSlotParents;
    [SerializeField] private Transform _pegSlotParents;

    public async void FillResult(List<int> guess, int blackPegs, int whitePegs, List<Sprite> options)
    {
        int size = _answerSlots.Count;

        // Fill Guess
        for (int i = 0; i < size; i++)
        {
            Sprite sprite = options[guess[i]];
            _answerSlots[i].Fill(sprite);//TODO
            await Task.Delay(100);
        }

        // Fill Pegs
        int index;
        for (index = 0; index < blackPegs; index++)
        {
            _pegSlots[index].Fill(UIResultPegSlotFill.Black);
        }

        for (int y = 0; y < whitePegs; y++)
        {
            _pegSlots[index + y].Fill(UIResultPegSlotFill.White);
        }

    }
    
    public UIResultRow CreateRow(int size, Transform parent)
    {
        _answerSlots = new List<UIResultSlot>();
        transform.SetParent(parent);
        transform.SetAsFirstSibling();
        transform.localScale = Vector3.one;

        for (int i = 0; i < size; i++)
        {
            _answerSlots.Add(Instantiate(_answerSlotPrefab, _answerSlotParents)
                .Setup()
                .Spawn());
        }

        for (int i = 0; i < size; i++)
        {
            _pegSlots.Add(Instantiate(_pegSlotPrefab, _pegSlotParents)
                .Setup()
                .Spawn());
        }

        return this;
    }
}
