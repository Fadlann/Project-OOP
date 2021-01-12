using UnityEngine;

namespace CombatCharacter.Enemy.AI
{
[CreateAssetMenu()]
public class AIAgentConfig : ScriptableObject 
{
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float dieForce = 10.0f;
    public float maxSightDistance = 5.0f;
    [SerializeField] private float maxDistToTarget = 2.0f;

    public float MaxDistToTarget{get {return maxDistToTarget;}}
}
}