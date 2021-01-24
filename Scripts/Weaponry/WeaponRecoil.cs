using UnityEngine;

namespace Weaponry
{
[RequireComponent(typeof(RaycastWeapon))]
public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private Vector2[] recoilPattern;
    [SerializeField] private float duration = .1f;
    
    private RaycastWeapon weapon;
    private Cinemachine.CinemachineImpulseSource cameraShake;
    private float time;
    private int index;
    private Transform target;
    private Animator rigController;

    private void Start() 
    {
        weapon = GetComponent<RaycastWeapon>();
        rigController = weapon.RigController;
        target = weapon.RaycastDestination;

        weapon.OnAttack += GenerateRecoil;
    }

    public void GenerateRecoil(Transform source)
    {
        time = duration;
        if (cameraShake)
            cameraShake.GenerateImpulse(Camera.main.transform.forward);
        
        index = (index + 1) % recoilPattern.Length;
        rigController.Play("weapon_recoil_" + weapon.WeaponName, 1, 0.0f);
    }

    private void Update() {
        if (time > 0)
        {
            target.position -= ((new Vector3(recoilPattern[index].x, recoilPattern[index].y)) * Time.deltaTime) / duration;
            time -= Time.deltaTime;
        }
    }
}
}