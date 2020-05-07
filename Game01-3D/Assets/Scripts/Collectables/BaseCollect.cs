using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollect : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Effect(collision.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Effect(other.gameObject);
        }
    }
    public virtual void Effect(GameObject other) {}
}
