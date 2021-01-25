using UnityEngine;

namespace CombatCharacter.Enemy
{
public class Enemy : CombatEntity 
{
    [Header("Colour Hit Change Effect")]
    [SerializeField] private float blinkIntensity = 10;
    [SerializeField] private float blinkDuration = 1;

    private SkinnedMeshRenderer skinnedMeshRenderer;
    private float blinkTimer;

    protected override void Start() 
    {
        base.Start();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject, 10f);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        if (CurrentHealth <= .0f) 
            Die();
        blinkTimer = blinkDuration;
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