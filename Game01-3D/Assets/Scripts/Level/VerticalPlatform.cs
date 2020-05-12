using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    public Transform topPoint, bottomPoint;
    public float speed = 30;
    public float closeEnough = 0.5f;
    public bool onTop;
    bool moving;
    Transform target;
    private void Start()
    {
        target = onTop ? bottomPoint : topPoint;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < closeEnough)
        {
            moving = false;
            onTop = target == topPoint ? true : false;
            target = onTop ? bottomPoint : topPoint;
        }
        if (moving)
        {
            Vector3 Movement = target.position - transform.position;
            Movement = Movement.normalized * speed * Time.deltaTime;
            transform.Translate(Movement);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        moving = true;
    }
}
