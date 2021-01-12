using UnityEngine;

namespace CombatCharacter.Player.Weaponry
{
public class SaberCombat : MonoBehaviour 
{
    [SerializeField] float timeBetweenAttacks = 1f;
    [Header("For debugging")]
    [SerializeField] bool attacking;
    
    Saber saber;
    ActiveWeapon weapon;
    Animator anim;

    float attackDelay;

    private void Start() 
    {
        weapon = GetComponent<ActiveWeapon>();
        anim = GetComponent<Animator>();
        saber = GetComponentInChildren<Saber>();
        weapon.onRifleOn +=  ToggleSaberOff;
        weapon.onRifleOff += ToggleSaberOn;
    }    

    private void Update() 
    {
        if(Time.time > attackDelay)
        {
            attacking = false;
            anim.SetBool("attacking", false);
        }

        if (!weapon || !weapon.Aiming)    
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                attacking = true;
                anim.SetTrigger("attack");
                anim.SetBool("attacking", true);
                attackDelay = Time.time + timeBetweenAttacks;
            }
        }
    }

    private void ToggleSaberOff()
    {
        saber.ToggleWeaponOnOff();
    }

    private void ToggleSaberOn()
    {
        saber.Invoke("ToggleWeaponOnOff", 1f);
    }

    public void Hit()
    {

    }
}
}