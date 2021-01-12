using UnityEngine;
using UnityEngine.AI;

namespace CombatCharacter.Enemy.AI
{
public class AIAgent : MonoBehaviour {
    
    public AIStateMachine stateMachine;
    public AIStateID initialState;
    public NavMeshAgent navMeshAgent;
    public AIAgentConfig config;

    public Ragdoll ragdoll;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public UIHealthBar healthBar;

    public Transform playerTransform;

    private void Start() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIDeathState());
        stateMachine.RegisterState(new AIIdleState());

        stateMachine.ChangeState(initialState);
    }

    private void Update() 
    {
        stateMachine.Update();
    }
}
}