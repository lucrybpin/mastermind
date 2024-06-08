using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameService
{
    [SerializeField] private List<int> _possibleValues;
    [SerializeField] private List<int> _solution;
    [SerializeField] private List<int> _guess;

    public GameService()
    {
    }

    public void NewGame(List<int> possibleValues, int guessSize, bool allowRepeat)
    {
        _possibleValues = possibleValues;
        _solution = GenerateSolution(possibleValues, guessSize, allowRepeat);
        _guess = new List<int>(guessSize);
    }


    public (int,int) Guess() {
        return Guess(_guess);
    }

    public (int, int) Guess(List<int> guess){

        if (guess.Count != _solution.Count)
            throw new System.ArgumentException("The length of the guess must match the length of the secret code.");

        int blackPegs = 0;
        int whitePegs = 0;

        // Lists to keep track of the indices already matched
        bool[] secretMatched = new bool[_solution.Count];
        bool[] guessMatched = new bool[_solution.Count];
        
        // First pass: find all black pegs
        for (int i = 0; i < _solution.Count; i++)
        {
            if (guess[i] == _solution[i])
            {
                blackPegs++;
                secretMatched[i] = true;
                guessMatched[i] = true;
            }
        }

        // Second pass: find all white pegs
        for (int i = 0; i < guess.Count; i++)
        {
            if (!guessMatched[i])
            {
                for (int j = 0; j < _solution.Count; j++)
                {
                    if (!secretMatched[j] && guess[i] == _solution[j])
                    {
                        whitePegs++;
                        secretMatched[j] = true;
                        break;
                    }
                }
            }
        }

        return (blackPegs, whitePegs);

    }

    private List<int> GenerateSolution(List<int> possibleValues, int size, bool allowRepeat)
    {
        List<int> sequence = new List<int>();

        // Repetition
        if (allowRepeat)
        {
            for (int i = 0; i < size; i++)
                sequence.Add(possibleValues[Random.Range(0, possibleValues.Count)]);

            return sequence;
        }

        if (size > possibleValues.Count)
            throw new System.ArgumentException("Sequence size can`t be greater than possible values");


        // No Repetition
        List<int> pickList = new List<int>(possibleValues);
        for (int i = 0; i < size; i++)
        {
            int index = Random.Range(0, pickList.Count);
            sequence.Add(pickList[index]);
            pickList.RemoveAt(index);
        }
        return sequence;
    }
}
