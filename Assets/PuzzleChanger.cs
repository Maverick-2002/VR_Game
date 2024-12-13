using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleChanger : MonoBehaviour
{
    public List<GameObject> planetPuzzles;
    private int currentPuzzleIndex = 0;
    void Start()
    {
        for (int i = 1; i < planetPuzzles.Count; i++)
        {
            planetPuzzles[i].SetActive(false);
        }
        planetPuzzles[currentPuzzleIndex].SetActive(true);
    }
    public void OnNextButtonClicked()
    {
        SwitchToNextPuzzle();
    }
    private void SwitchToNextPuzzle()
    {
        planetPuzzles[currentPuzzleIndex].SetActive(false);
        currentPuzzleIndex++;
        if (currentPuzzleIndex < planetPuzzles.Count)
        {
            planetPuzzles[currentPuzzleIndex].SetActive(true);
        }
    }
}
