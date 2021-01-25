using System.Collections;
using UnityEngine;
using Weaponry;
using System.Collections.Generic;

namespace CombatCharacter.Player
{
public class PlayerWeapon : WeaponHandler 
{
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))    
            Equip(weapons[WeaponType.Primary]);
        else if (Input.GetKeyDown(KeyCode.Alpha2))    
            Equip(weapons[WeaponType.Secondary]);
    }

    private void LateUpdate() 
    {
        if (Armless)
            return;
        
        if(Input.GetButtonDown("Fire1"))
            CurWeapon.StartAttack();

        if(CurWeapon != null && CurWeapon.IsAttacking)
            CurWeapon.UpdateAttack();
        
        if(Input.GetButtonUp("Fire1"))
            CurWeapon.StopAttack();
    }
}
}