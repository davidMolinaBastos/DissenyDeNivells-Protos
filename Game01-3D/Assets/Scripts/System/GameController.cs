using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    int score = 0;
    int plants = 0;
    List<int> keyIDs = new List<int>();
    public int plantsToWin = 5;
    [Header("Menu Variables")]
    public Text shield;
    public Text ammo1;
    public Text ammo2;
    public Text life;
    public Text weirdPlants;

    PlayerController PC;
    public void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        UpdateUI();
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void GameWon()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void UpdateUI()
    {
        shield.text = "Shield: " + PC.GetShield();
        ammo1.text = "Ammo1:" + PC.GetMag1() + " / " + PC.GetAmmo1();
        ammo2.text = "Ammo2:" + PC.GetMag2() + " / " + PC.GetAmmo2();
        life.text = "HP: " + PC.GetHP();
        weirdPlants.text = "Plants: " + plants;
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
