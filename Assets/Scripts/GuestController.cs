using System.Collections.Generic;
using UnityEngine;

public class GuestController : MonoBehaviour
{
    [SerializeField] private UIGuessSlot _uiGuessSlotPrefab;
    [SerializeField] private List<UIGuessSlot> _guessSlots;
    [SerializeField] private List<UIGuessPiece> _guessPieces;

    public void SpawnSlots(int slots)
    {

    }
}
