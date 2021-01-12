using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.Animations.Rigging;
using System.Collections;

namespace CombatCharacter.Player.Weaponry
{
public class ActiveWeapon : MonoBehaviour 
{
    [Header("For Weapon Change Saving")]
    [SerializeField] Animator rigController;
    [SerializeField] bool rifleOn;
    [SerializeField] float delayBetweenChange = 1.5f;

    RaycastWeapon weapon;
    Animator anim;
    Cinemachine.CinemachineFreeLook playerCamera;
    float delayIsOver;

    public delegate void SwitchAction();
    public event SwitchAction onRifleOn;
    public event SwitchAction onRifleOff;

    public bool Aiming {get {return rifleOn;}}

    private void Start() 
    {
        anim = GetComponent<Animator>();
        weapon = GetComponentInChildren<RaycastWeapon>();
        anim.SetBool("aiming", rifleOn);
        rigController.SetBool("aiming", rifleOn);
        playerCamera = FindObjectOfType<Cinemachine.CinemachineFreeLook>();

        Equip();
    }  

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Q) && Time.time > delayIsOver)
        {
            rifleOn = !rifleOn;
            
            if (!rifleOn)
                onRifleOff();
            else
                onRifleOn();
            
            rigController.SetBool("aiming", rifleOn);
            anim.SetBool("aiming", rifleOn);
            weapon.StopFiring();

            delayIsOver = Time.time + delayBetweenChange;
        }
    }

    private void LateUpdate() 
    {
        if (rifleOn)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                weapon.StartFiring();
            }

            if(weapon.IsFiring)
                weapon.UpdateFiring(Time.deltaTime);
            
            weapon.UpdateBullets(Time.deltaTime);
            if(Input.GetButtonUp("Fire1"))
            {
                weapon.StopFiring();
            } 
        }
    } 

    void Equip()
    {
        rigController.Play("equip_" + weapon.WeaponName);
        if(weapon.Recoil)
        {
            weapon.Recoil.PlayerCamera = playerCamera;
            weapon.Recoil.RigController = rigController;
        }
    }
}
}