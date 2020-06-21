using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public float damage = 50f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(damage);
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Sword")
            gameObject.SetActive(false);
    }
}
