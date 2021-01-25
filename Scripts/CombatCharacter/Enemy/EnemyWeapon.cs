using UnityEngine;
using Weaponry;

namespace CombatCharacter.Enemy
{
public class EnemyWeapon : WeaponHandler 
{
    [SerializeField] private WeaponType weaponToUse;

    public void StartAttack() { CurWeapon.StartAttack(); }
    public void UpdateAttack() 
    { 
        if (CurWeapon != null && CurWeapon.IsAttacking)
            CurWeapon.UpdateAttack(); 
    }
    public void StopAttack() 
    { 
        if(CurWeapon != null)
            CurWeapon.StopAttack(); 
    }

    public void EquipWeapon() 
    {
        Equip(weapons[weaponToUse]);
        Invoke("StartAttack", 2);
    }
}
}