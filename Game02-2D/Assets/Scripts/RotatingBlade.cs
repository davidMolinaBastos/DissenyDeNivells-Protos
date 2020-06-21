using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlade : MonoBehaviour
{
    public float speed = 90f;

    private void Update()
    {
        transform.Rotate(0, 0, speed*Time.deltaTime, Space.World);
    }
}
