using UnityEngine;
using System;

namespace Weaponry
{
public enum WeaponType
{
    Primary = 0,
    Secondary = 1,
    Tertiary = 2
}

public interface IWeapon 
{
    string WeaponName {get;}
    WeaponType Type {get;}

    float Damage {get;}
    bool IsAttacking {get;}
    Transform Transform {get;}
    
    void StartAttack();
    void UpdateAttack();
    void StopAttack();

    event Action<Transform> OnAttack;
    event Action<RaycastHit> OnHit;
}
}