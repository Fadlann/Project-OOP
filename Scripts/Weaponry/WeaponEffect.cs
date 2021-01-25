using UnityEngine;
using Manager;

namespace Weaponry
{
[RequireComponent(typeof(IWeapon))]
public class WeaponEffect : MonoBehaviour 
{
    [SerializeField] private ParticleSystem[] attackEffects;
    [SerializeField] private ParticleSystem hitEffect;

    IWeapon weapon;

    private void Start() 
    {
        weapon = GetComponent<RaycastWeapon>();

        hitEffect = Instantiate(hitEffect, Vector3.zero, Quaternion.identity, transform);
        weapon.OnHit += OnHit;

        for (int i = 0; i < attackEffects.Length; i++)
        {
            attackEffects[i] = Instantiate(attackEffects[i], Vector3.zero, Quaternion.identity, transform);
        }
        weapon.OnAttack += OnAttack;
    }

    private void OnHit(RaycastHit hit)
    {
        hitEffect.transform.position = hit.point;
        hitEffect.transform.forward = hit.normal;
        hitEffect.Emit(1);
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