using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIResultsController : MonoBehaviour
{
    [SerializeField] private List<UIResultRow> _rows;
    [SerializeField] private UIResultRow _uiResultRowPrefab;
    [SerializeField] private Transform _resultsPanelTransform;
    [SerializeField] private GameData _gameData;
    private int index = 0;
    
    public void FillResult(List<int> guess, int blackPegs, int whitePegs)
    {
        _rows[index].FillResult(guess, blackPegs, whitePegs, _gameData.Options);
        index++;
    }

    public async void CreateRows(int numberOfTries, int SecretSize, GameData gameData)
    {
        _rows = new List<UIResultRow>();
        for (int i = 0; i < numberOfTries; i++)
        {
            _rows.Add(Instantiate(_uiResultRowPrefab)
                .CreateRow(SecretSize, _resultsPanelTransform));
            await Task.Delay(100);
        }
        _gameData = gameData;
    }

}
