using UnityEngine;
using CombatCharacter.Player.Weaponry;

namespace CombatCharacter.Enemy
{
[RequireComponent(typeof(Health))]
public class HitBox : MonoBehaviour 
{
    Health health;

    public Health Health {get{return health;} set{health = value;}}

    public void OnRaycastHit(RaycastWeapon weapon, Vector3 direction)
    {
        health.TakeDamage(weapon.Damage, direction);
    }
}
}