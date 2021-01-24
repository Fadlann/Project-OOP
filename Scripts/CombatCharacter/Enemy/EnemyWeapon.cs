using UnityEngine;
using Weaponry;

namespace CombatCharacter.Enemy
{
public class EnemyWeapon : MonoBehaviour 
{
    [SerializeField] Animator rigController;
    [SerializeField] bool rifleOn;

    public RaycastWeapon Weapon {get; private set;}
    Animator anim;

    private void Start() 
    {
        anim = GetComponent<Animator>();
        anim.SetBool("aiming", rifleOn);

        Weapon = GetComponentInChildren<RaycastWeapon>();
        Weapon.Entity = GetComponent<CombatEntity>();
        
        rigController.SetBool("aiming", rifleOn);

        Equip();
    }  

    void Equip()
    {
        rigController.Play("equip_" + Weapon.WeaponName);
        Weapon.RigController = rigController;
    }
}
}