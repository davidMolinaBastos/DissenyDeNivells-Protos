using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInputs : MonoBehaviour
{
    public KeyCode depleteAmmo = KeyCode.I;
    public KeyCode damagePlayer = KeyCode.O;

    PlayerController PC;
    private void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(depleteAmmo))
        {
            PC.ChangeAmmo(PC.GetAmmo1(), true);
            PC.ChangeAmmo(PC.GetAmmo2(), false);
        }
        if (Input.GetKeyDown(damagePlayer))
            PC.Damage(PC.GetHP() / 2);
    }
}
