using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : BaseCollect
{
    public int value = 50;
    public override void Effect(GameObject other)
    {
        if (other.GetComponent<PlayerController>().RestoreHP(value))
            gameObject.SetActive(false);
    }
}
