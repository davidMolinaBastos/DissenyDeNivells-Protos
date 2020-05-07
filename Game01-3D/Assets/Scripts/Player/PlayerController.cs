using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    float Yaw;
    float Pitch;
    float VerticalSpeed = 0.0f;
    int HP = 100;
    int Shield = 100;
    float InmunityCT = 0;
    int mag1, mag2, ammo1, ammo2;
    bool onGround = true;

    PlayerStats PS;
    CharacterController CC;

    public GameController GC;

    [HideInInspector] public int InmunityItems = 0;
    public Transform PitchController;
    public bool InvertedYaw = false;
    public bool InvertedPitch = true;
    public LayerMask ShootLayerMask;
    public WeaponObject weapon1, weapon2;


    void Awake()
    {
        Yaw = transform.rotation.eulerAngles.y;
        Pitch = PitchController.localRotation.eulerAngles.x;

        PS = GetComponent<PlayerStats>();
        CC = GetComponent<CharacterController>();

        mag1 = weapon1.MagazineSize;
        ammo1 = weapon1.MaxAmmo;
        mag2 = weapon2.MagazineSize;
        ammo2 = weapon2.MaxAmmo;
    }

    void Update()
    {
        if (InmunityCT > 0)
            InmunityCT -= Time.deltaTime;
        //Speed Calculations
        VerticalSpeed += Physics.gravity.y * Time.deltaTime;

        //Pitch calculated
        float MouseAxisY = Input.GetAxis("Mouse Y");
        if (InvertedPitch)
            Pitch += MouseAxisY * -PS.PitchRotationalSpeed * Time.deltaTime;
        else
            Pitch += MouseAxisY * PS.PitchRotationalSpeed * Time.deltaTime;
        Pitch = Mathf.Clamp(Pitch, PS.MinPitch, PS.MaxPitch);

        //Yaw Calculated
        float MouseAxisX = Input.GetAxis("Mouse X");
        Yaw += MouseAxisX * PS.YawRotationalSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, Yaw, 0);
        PitchController.localRotation = Quaternion.Euler(Pitch, 0, 0);

        float YawInRadians = Yaw * Mathf.Deg2Rad;
        float Yaw90InRadians = (Yaw + 90) * Mathf.Deg2Rad;

        Vector3 Forward = new Vector3(Mathf.Sin(YawInRadians), 0, Mathf.Cos(YawInRadians));
        Vector3 Right = new Vector3(Mathf.Sin(Yaw90InRadians), 0, Mathf.Cos(Yaw90InRadians));
        Vector3 Movement = Vector3.zero;

        //Input Detection
        if (ActionControlls.ForwardPressed())
            Movement = Forward;
        else if (ActionControlls.BackPressed())
            Movement = -Forward;

        if (ActionControlls.RightPressed())
            Movement += Right;
        else if (ActionControlls.LeftPressed())
            Movement -= Right;

        if (onGround && ActionControlls.JumpActionPressed())
            VerticalSpeed = PS.JumpSpeed;

        if (ActionControlls.FireActionPressed() && CanShoot())
            Shoot();
        else if (ActionControlls.Fire2ActionPressed() && CanShoot())
            AltShoot();

        if (ActionControlls.InmunityKeyPressed() && InmunityItems > 0)
        {
            InmunityItems--;
            InmunityCT = PS.InmunityTime;
        }

        if (ActionControlls.ReloadPressed() && (mag1 < weapon1.MagazineSize || mag2 < weapon2.MagazineSize ))
            Reload();

        //Move The Character
        Movement.Normalize();
        Movement.y = VerticalSpeed * Time.deltaTime;
        if (VerticalSpeed >= 0.5f || VerticalSpeed <= -0.5f)
        {
            Movement.x *= PS.InAirSpeedFactor;
            Movement.z *= PS.InAirSpeedFactor;
        }
        Movement *= Time.deltaTime * PS.Speed;
        CollisionFlags CollisionFlags = CC.Move(Movement);
        if ((CollisionFlags & CollisionFlags.Below) != 0)
        {
            onGround = true;
            VerticalSpeed = 0;
        }
        else
            onGround = false;
        if ((CollisionFlags & CollisionFlags.Above) != 0 && VerticalSpeed > 0)
            VerticalSpeed = 0f;
    }
    void Shoot()
    {
        mag1--;
        Ray CameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit RaycastHit;
        if (Physics.Raycast(CameraRay, out RaycastHit, 200, ShootLayerMask.value))
        {
            switch (RaycastHit.collider.gameObject.tag)
            {
                default:
                    break;
            }
        }
    }
    void AltShoot()
    {
        mag2--;
        Ray CameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit RaycastHit;
        if (Physics.Raycast(CameraRay, out RaycastHit, 200, ShootLayerMask.value))
        {
            switch (RaycastHit.collider.gameObject.tag)
            {
                default:
                    break;
            }
        }
    }
    bool CanShoot()
    {
        return true;
    }
    void Reload()
    {
        if(mag1 == weapon1.MagazineSize)
        {
            int diference1 = weapon1.MagazineSize - mag1;
            ammo1 -= diference1;
            mag1 = weapon1.MagazineSize;
        }
        if (mag2 == weapon2.MagazineSize)
        {
            int diference2 = weapon2.MagazineSize - mag2;
            ammo2 -= diference2;
            mag2 = weapon2.MagazineSize;
        }
        GC.UpdateUI();
    }

    //Items
    public bool ChangeAmmo(int value, bool mainW)
    {
        bool used = false;
        if (mainW && ammo1 < weapon1.MaxAmmo)
        {
            ammo1 += value;
            Mathf.Clamp(ammo1, 0, weapon1.MaxAmmo);
            used = true;
        }
        else if(!mainW && ammo2 < weapon2.MaxAmmo)
        {
            ammo2 += value;
            Mathf.Clamp(ammo2, 0, weapon2.MaxAmmo);
            used = true;
        }
        GC.UpdateUI();
        return used;
    }

    public bool RestoreHP(int value)
    {
        bool used = false;
        if (HP < PS.MaxLife)
        {
            HP += value;
            Mathf.Clamp(HP, 0, PS.MaxLife);
            used = true;
        }
        GC.UpdateUI();
        return used;
    }

    public bool RestoreShield(int value)
    {
        bool used = false;
        if(Shield < PS.MaxShield)
        {
            Shield += value;
            Mathf.Clamp(Shield, 0, PS.MaxShield);
            used = true;
        }
        GC.UpdateUI();
        return used;
    }
    public void AddInmunityItem(int value)
    {
        InmunityItems += value;
        GC.UpdateUI();
    }
    public void Damage(int value)
    {
        if(InmunityCT <= 0)
            if(Shield > 0)
                if(Shield < value * PS.ShieldAbsortion)
                {
                    HP -= value + Shield;
                    Shield = 0;
                }
                else
                {
                    Shield -= (int)(value * PS.ShieldAbsortion);
                    Mathf.Clamp(Shield, 0, Shield);
                    HP -= (int)(value * 1- PS.ShieldAbsortion);
                }
            else
                HP -= value;
        if (HP <= 0)
        {
            GC.GameOver();
        }
        GC.UpdateUI();
    }
    public bool Inmune()
    {
        return InmunityCT > 0f;
    }
    public int GetHP()
    {
        return HP;
    }
    public int GetShield()
    {
        return Shield;
    }
    public int GetMag1()
    {
        return mag1;
    }
    public int GetMag2()
    {
        return mag2;
    }
    public int GetAmmo1()
    {
        return ammo1;
    }
    public int GetAmmo2()
    {
        return ammo2;
    }
    public int GetInmunityItems()
    {
        return InmunityItems;
    }
}
