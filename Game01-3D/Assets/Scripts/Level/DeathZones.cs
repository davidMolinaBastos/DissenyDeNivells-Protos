using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZones : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver();
    }
}
