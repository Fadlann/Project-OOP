using System.Collections;
using UnityEngine;
using Weaponry;
using System.Collections.Generic;

namespace CombatCharacter.Player
{
public class PlayerWeapon : MonoBehaviour 
{
    [SerializeField] Animator rigController;
    [SerializeField] private Transform crosshair;
    
    public bool Armless {get; private set;}
    IWeapon curWeapon;
    Animator anim;
    private Dictionary<WeaponType, IWeapon> weapons = new Dictionary<WeaponType, IWeapon>();

    private void Start() 
    {
        Armless = true;
        anim = GetComponent<Animator>();

        IWeapon[] temp = GetComponentsInChildren<IWeapon>();
        foreach (IWeapon weapon in temp)
        {
            if (weapons.ContainsKey(weapon.Type))
                Debug.LogError(weapon.Type + " is already assigned");
            
            weapons[weapon.Type] = weapon;
            if (weapon is RaycastWeapon)
                SetUp(weapon as RaycastWeapon);          
        }
        // for debug
        rigController.SetBool("armless", Armless);
        anim.SetBool("armless", Armless);
    }  

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
            curWeapon.StartAttack();

        if(curWeapon != null && curWeapon.IsAttacking)
            curWeapon.UpdateAttack();
        
        if(Input.GetButtonUp("Fire1"))
            curWeapon.StopAttack();
    }

    void SetUp(RaycastWeapon weapon)
    {        
        weapon.Entity = GetComponent<CombatEntity>();
        weapon.RigController = rigController;
        weapon.RaycastDestination = crosshair;
    }

    void Equip(IWeapon weapon)
    {        
        if (curWeapon != null)
            curWeapon.StopAttack();
        
        Armless = false;
        anim.SetBool("armless", false);
        StartCoroutine(SwitchWeapon(weapon));
    }

    IEnumerator SwitchWeapon(IWeapon weaponToActivate)
    {
        yield return StartCoroutine(HolsterWeapon(curWeapon));
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
        curWeapon = weapon;
    }
}
}