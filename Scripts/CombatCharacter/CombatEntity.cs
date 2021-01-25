using UnityEngine;
using Manager;

namespace CombatCharacter
{
public abstract class CombatEntity : MonoBehaviour{
    [SerializeField] private float maxHealth = 100;

    public delegate void DeathAction();
    public event DeathAction OnDeath;

    public delegate void HitAction();
    public event HitAction OnHit;

    public float CurrentHealth{ get; private set; }
    public bool Dead{ get; private set; }
    public float MaxHealth {get {return maxHealth;} }

    protected virtual void Start()
    {
        CurrentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (OnHit != null)
            OnHit();
    }

    protected virtual void Die()
    {
        Dead = true;
        if (OnDeath != null)
            OnDeath();
    }
}
}