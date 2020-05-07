using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionControlls
{
    public static KeyCode jumpKey = KeyCode.Space;
    public static KeyCode inmunityKey = KeyCode.LeftShift;
    public static KeyCode fire1Key = KeyCode.Mouse0;
    public static KeyCode fire2Key = KeyCode.Mouse1;
    public static KeyCode itemKey = KeyCode.E;
    public static KeyCode ForwardKey = KeyCode.W;
    public static KeyCode BackKey = KeyCode.S;
    public static KeyCode LeftKey = KeyCode.A;
    public static KeyCode RightKey = KeyCode.D;
    public static KeyCode ReloadKey = KeyCode.R;

    public static bool InmunityKeyPressed()
    {
        return Input.GetKeyDown(inmunityKey);
    }
    public static bool JumpActionPressed()
    {
        return Input.GetKeyDown(jumpKey);
    }
    public static bool FireActionPressed()
    {
        return Input.GetKeyDown(fire1Key);
    }
    public static bool Fire2ActionPressed()
    {
        return Input.GetKeyDown(fire2Key);
    }
    public static bool itemActionPressed()
    {
        return Input.GetKeyDown(itemKey);
    }
    public static bool ForwardPressed()
    {
        return Input.GetKey(ForwardKey);
    }
    public static bool BackPressed()
    {
        return Input.GetKey(BackKey);
    }
    public static bool LeftPressed()
    {
        return Input.GetKey(LeftKey);
    }
    public static bool RightPressed()
    {
        return Input.GetKey(RightKey);
    }
    public static bool ReloadPressed()
    {
        return Input.GetKeyDown(ReloadKey);
    }

}
