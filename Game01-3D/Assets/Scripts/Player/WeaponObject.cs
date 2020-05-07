using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class WeaponObject : ScriptableObject
{
    [Header("Stats")]
    public float Cadenece;
    public float Damage;
    public int MagazineSize;
    public int MaxAmmo;
}
