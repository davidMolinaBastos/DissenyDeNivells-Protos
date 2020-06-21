using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text HPtext;
    public PlayerController pc;
    public Text InmunityText;

    float width;
    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UpdateGUI()
    {
        HPtext.text = "HP: " + pc.GetHP();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pc.InmunityToggle();
            InmunityText.text = "(Debug) - E ToggleInmunity: " + pc.GetInmunity();
        }
    }
}
