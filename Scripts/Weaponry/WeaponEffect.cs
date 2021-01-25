using UnityEngine;

namespace Weaponry
{
[RequireComponent(typeof(IWeapon))]
public class WeaponEffect : MonoBehaviour 
{
    [SerializeField] private ParticleSystem[] attackEffects;
    [SerializeField] private ParticleSystem[] hitEffects;

    IWeapon weapon;

    private void Start() 
    {
        weapon = GetComponent<RaycastWeapon>();

        for (int i = 0; i < hitEffects.Length; i++)
        {
            hitEffects[i] = Instantiate(hitEffects[i], Vector3.zero, Quaternion.identity, transform);
        }
        weapon.OnHit += OnHit;

        for (int i = 0; i < attackEffects.Length; i++)
        {
            attackEffects[i] = Instantiate(attackEffects[i], Vector3.zero, Quaternion.identity, transform);
        }
        weapon.OnAttack += OnAttack;
    }

    private void OnHit(RaycastHit hit)
    {
        foreach(var effect in hitEffects)
        {
            effect.transform.position = hit.point;
            effect.transform.forward = hit.normal;
            effect.Emit(1);
        }
    }

    private void OnAttack(Transform source)
    {
        foreach(var effect in attackEffects)
        {
            effect.transform.position = source.position;
            effect.transform.forward = source.forward;
            effect.Emit(1);
        }
    }
}
}