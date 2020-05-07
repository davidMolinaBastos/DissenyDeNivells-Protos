using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float YawRotationalSpeed = 360.0f;
    public float PitchRotationalSpeed = 180.0f;
    public float MinPitch = -80.0f;
    public float MaxPitch = 50.0f;
    public float Speed = 10.0f;
    public float JumpSpeed = 10.0f;
    public float InAirSpeedFactor = 0.9f;
    public float MaxLife = 100f;
    public float MaxShield = 100f;
    public float InmunityTime = 5f;
    [Range(0, 1)] public float ShieldAbsortion = 0.75f;
}
