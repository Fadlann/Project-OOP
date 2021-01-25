using System.Collections;
using UnityEngine;
using Weaponry;
using System.Collections.Generic;

namespace CombatCharacter
{
public class WeaponHandler : MonoBehaviour 
{
    [SerializeField] Animator rigController;
    [SerializeField] private Transform target;
    
    public bool Armless {get; private set;}
    public IWeapon CurWeapon {get; private set;}
    protected Animator Anim;
    protected Dictionary<WeaponType, IWeapon> weapons = new Dictionary<WeaponType, IWeapon>();

    protected virtual void Awake() 
    {
        Armless = true;
        Anim = GetComponent<Animator>();

        IWeapon[] temp = GetComponentsInChildren<IWeapon>();
        foreach (IWeapon weapon in temp)
        {
            if (weapons.ContainsKey(weapon.Type))
                Debug.LogError(weapon.Type + " is already assigned");
            
            weapons[weapon.Type] = weapon;
            if (weapon is RaycastWeapon)
                SetUp(weapon as RaycastWeapon); 
        }
    }  

    void SetUp(RaycastWeapon weapon)
    {        
        weapon.Entity = GetComponent<CombatEntity>();
        weapon.RigController = rigController;
        weapon.RaycastDestination = target;
    }

    protected void Equip(IWeapon weapon)
    {        
        if (CurWeapon != null)
            CurWeapon.StopAttack();
        
        Armless = false;
        Anim.SetBool("armless", false);
        StartCoroutine(SwitchWeapon(weapon));
    }

    IEnumerator SwitchWeapon(IWeapon weaponToActivate)
    {
        yield return StartCoroutine(HolsterWeapon(CurWeapon));
        yield return StartCoroutine(ActivateWeapon(weaponToActivate));
    }

    IEnumerator HolsterWeapon(IWeapon weapon)
    {
        if (weapon != null)
        {
            rigController.Play("unequip_" + weapon.WeaponName);
            yield return new WaitForFixedUpdate();
            do {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
        }
    }

    IEnumerator ActivateWeapon(IWeapon weapon)
    {
        if (weapon != null)
        {
            rigController.Play("equip_" + weapon.WeaponName);//("equip");
            yield return new WaitForFixedUpdate();
            do {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
        }
        CurWeapon = weapon;
    }
}
}