using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : BaseCollect
{
    public int value = 50;
    public override void Effect(GameObject other)
    {
        if (other.GetComponent<PlayerController>().RestoreShield(value))
            gameObject.SetActive(false);
    }
}
