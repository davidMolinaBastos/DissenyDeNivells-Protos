using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int doorID;
    public Animation doorAnim;
    public AnimationClip doorOpen;
    public AnimationClip doorExit;
    private void Start()
    {
        doorAnim = GetComponent<Animation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            if(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().CheckKey(doorID))
            {
                doorAnim.clip = doorOpen;
                doorAnim.Play();
            }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            doorAnim.clip = doorExit;
            doorAnim.Play();
        }
    }
}

