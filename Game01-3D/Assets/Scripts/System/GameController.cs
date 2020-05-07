using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int score = 0;
    int plants = 0;

    public int plantsToWin = 5;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
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
}
