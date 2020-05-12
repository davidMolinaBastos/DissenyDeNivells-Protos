using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed = 30;
    public float closeEnough = 0.5f;
    Transform target;
    private void Start()
    {
        target = pointA;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < closeEnough)
            target = target == pointA ? pointB : pointA;

        Vector3 Movement = target.position - transform.position;
        Movement = Movement.normalized * speed * Time.deltaTime;
        transform.Translate(Movement);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;
        other.transform.parent = transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player")
            return;
        other.transform.parent = null;
    }
}
