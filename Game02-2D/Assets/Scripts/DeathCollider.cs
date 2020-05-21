﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().GameOver();
    }
}
