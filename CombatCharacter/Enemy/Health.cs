using UnityEngine;
using CombatCharacter.Enemy.AI;

namespace CombatCharacter.Enemy
{
public class Health : MonoBehaviour 
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float blinkIntensity = 10;
    [SerializeField] float blinkDuration = 1;

    SkinnedMeshRenderer skinnedMeshRenderer;
    UIHealthBar healthBar;

    float curHealth;
    float blinkTimer;

    AIAgent agent;
    bool dead;

    public delegate void DeathAction();
    public event DeathAction OnDeath;

    public float CurrentHealth{get {return curHealth;}}
    public bool Dead{get {return dead;}}

    private void Start() 
    {
        agent = GetComponent<AIAgent>();
        curHealth = maxHealth;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UIHealthBar>();

        if(agent)
        {
            foreach (var rigidbody in agent.ragdoll.Rigidbodies)
            {
                HitBox hitbox = rigidbody.gameObject.AddComponent<HitBox>();
                hitbox.Health = this;
            }
        }
    }

    public void TakeDamage(float amount, Vector3 direction)
    {
        curHealth -= amount;

        if (healthBar)
        {
            healthBar.SetHealthBarPercentage(curHealth / maxHealth);
        }
        if (curHealth <= .0f)
        {
            Die(direction);
        }

        blinkTimer = blinkDuration;
    }

    void Die(Vector3 direction)
    {
        AIDeathState deathState = agent.stateMachine.GetState(AIStateID.Death) as AIDeathState;
        deathState.direction = direction;
        agent.stateMachine.ChangeState(AIStateID.Death);
    }

    private void Update() 
    {
        if (skinnedMeshRenderer)
        {
            blinkTimer -= Time.deltaTime;    
            float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
            float intensity = (lerp * blinkIntensity) + 1f;
            skinnedMeshRenderer.material.color = Color.white * intensity;
        }
    }

}
}