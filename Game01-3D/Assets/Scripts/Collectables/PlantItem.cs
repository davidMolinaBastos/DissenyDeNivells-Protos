using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantItem : BaseCollect
{
    public int value = 1;
    public override void Effect(GameObject other)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ChangePlants(1);
        gameObject.SetActive(false);
    }
}
