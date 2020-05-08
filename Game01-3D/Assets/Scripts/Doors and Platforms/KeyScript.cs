using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public int keyID;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().CheckKey(keyID);
    }
}
