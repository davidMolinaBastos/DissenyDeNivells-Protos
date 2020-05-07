using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoItem : BaseCollect
{
    public int value = 100;
    public bool weapon1 = true;
    public override void Effect(GameObject other)
    {
        if (other.GetComponent<PlayerController>().ChangeAmmo(value, weapon1))
            gameObject.SetActive(false);
    }
}
