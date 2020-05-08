using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int score = 0;
    int plants = 0;
    List<int> keyIDs = new List<int>();
    public int plantsToWin = 5;
    public void GameOver()
    {

    }
    public void GameWon()
    {

    }
    public void UpdateUI()
    {

    }
    public void ChangePlants(int value)
    {
        plants += value;
        if (plants >= plantsToWin)
            GameWon();
        UpdateUI();
    }
    public void ChangeScore(int value)
    {
        score += value;
        Mathf.Clamp(score, 0, score);
        UpdateUI();
    }
    public void AddKey(int id)
    {
        keyIDs.Add(id);
    }
    public bool CheckKey(int id)
    {
        foreach (int i in keyIDs)
            if (i == id)
                return true;
        return false;
    }
}
