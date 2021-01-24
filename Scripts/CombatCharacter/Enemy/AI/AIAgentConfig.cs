using UnityEngine;

namespace CombatCharacter.Enemy.AI
{
[CreateAssetMenu()]
public class AIAgentConfig : ScriptableObject 
{
    [SerializeField] private float maxTime = 1.0f;
    [SerializeField] private float maxDistance = 1.0f;
    [SerializeField] private float dieForce = 10.0f;
    [SerializeField] private float maxSightDistance = 5.0f;

    #region getter
    public float MaxTime {get {return maxTime;}}
    public float MaxDistance {get {return maxDistance;}}
    public float DieForce {get {return dieForce;}}
    public float MaxSightDistance{ get {return maxSightDistance;}}
    #endregion
}
}