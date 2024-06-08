using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameData : ScriptableObject 
{
    public int SecretSize;
    public int NumberOfGuess;
    public List<Sprite> Options;

    public Sprite GetOption(int index)
    {
        return Options[index];
    }
}
