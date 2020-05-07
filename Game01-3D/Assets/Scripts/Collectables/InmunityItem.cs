using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmunityItem : BaseCollect
{
    public int value = 1;
    public override void Effect(GameObject other)
    {
        other.GetComponent<PlayerController>().AddInmunityItem(value);
        gameObject.SetActive(false);
    }
}
