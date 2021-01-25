using UnityEngine;
using System.Collections;
using Manager;

namespace Weaponry
{
[RequireComponent(typeof(RaycastWeapon))]
public class WeaponAmmo : MonoBehaviour 
{
    [SerializeField] private int maxAmmo = 20;
    [SerializeField] private int ammoLeft = 20;
    [SerializeField] private float reloadDuration = 1;
    [SerializeField] private AudioClip ejectSound;
    [SerializeField] private AudioClip loadSound;
    [SerializeField] private AudioClip chamberingSound;

    private WeaponAnimationEvents animationEvents;
    private Animator rigController;
    private int ammoCount;
    private bool reloading;

    private void Start() 
    {
        ammoCount = maxAmmo;   

        RaycastWeapon weapon = GetComponent<RaycastWeapon>();
        rigController = weapon.RigController;
        animationEvents = rigController.GetComponent<WeaponAnimationEvents>();
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
        
        GetComponent<RaycastWeapon>().OnFiring += OnFiringBullet;
    }

    private bool OnFiringBullet()
    {
        if (reloading)
            return false;
        
        if (ammoCount <= 0)
        {
            Reload();
        }
        ammoCount--;
        return true;
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    public void Reload()
    {
        StartCoroutine("StartReloading");
    }

    private IEnumerator StartReloading()
    {
        if(reloading || ammoLeft <= 0 || ammoCount == maxAmmo)
            yield return null;
        else
        {
            reloading = true;

            rigController.SetTrigger("reload");
            yield return new WaitForFixedUpdate();
            do {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);
            RefillAmmo();
        }
    }

    private void OnAnimationEvent(string eventName)
    {
        Debug.Log(eventName);
        switch (eventName)
        {
            case "eject_magazine":
                AudioManager.Instance.PlaySound(ejectSound, transform.position);
                break;
            case "load_magazine":
                AudioManager.Instance.PlaySound(loadSound, transform.position);
                break;
            case "cock_magazine":
                AudioManager.Instance.PlaySound(chamberingSound, transform.position);
                break;
            case "reload_finished":
                reloading = false;
                break;
        }
    }

    private void RefillAmmo()
    {
        int ammoToGive = maxAmmo - ammoCount;
        ammoLeft -= ammoToGive;
        if(ammoLeft < 0)
        {
            ammoLeft = 0;
            ammoToGive = ammoLeft;
        }
        ammoCount += ammoToGive;
    }
}
}