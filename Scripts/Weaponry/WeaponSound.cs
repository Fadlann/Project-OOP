using UnityEngine;
using Manager;

namespace Weaponry
{
[RequireComponent(typeof(WeaponSound))]
public class WeaponSound : MonoBehaviour 
{
    [SerializeField] private AudioClip attackSound;
    private IWeapon weapon;

    private void Start() 
    {
        weapon = GetComponent<IWeapon>();
        weapon.OnAttack += PlaySound;
    }

    private void PlaySound(Transform source)
    {
        AudioManager.Instance.PlaySound(attackSound, source.position);
    }
}
}