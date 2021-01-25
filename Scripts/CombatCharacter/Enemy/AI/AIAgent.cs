using UnityEngine;
using UnityEngine.AI;

namespace CombatCharacter.Enemy.AI
{
public class AIAgent : MonoBehaviour {
    [SerializeField] private AIAgentConfig config;
    [SerializeField] private AIStateID initialState;
    private Animator animator;

    public AIAgentConfig Config {get {return config;}}
    public Transform PlayerTransform {get; private set;}
    public AIStateMachine StateMachine {get; private set;}
    public EnemyWeapon ActiveWeapon {get; private set;}
    public NavMeshAgent NavMeshAgent {get; private set;}
    public SkinnedMeshRenderer SkinnedMeshRenderer {get; private set;}

    private void Start() 
    {
        animator = GetComponent<Animator>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        NavMeshAgent = GetComponent<NavMeshAgent>();
        SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        ActiveWeapon = GetComponent<EnemyWeapon>();

        GetComponent<Enemy>().OnDeath += SetToDeathState;

        StateMachine = new AIStateMachine(this);
        StateMachine.RegisterState(new AIChasePlayerState());
        StateMachine.RegisterState(new AIDeathState());
        StateMachine.RegisterState(new AIIdleState());
        StateMachine.RegisterState(new AIShootingState());

        StateMachine.ChangeState(initialState);
    }

    private void Update() 
    {
        animator.SetFloat("Speed", NavMeshAgent.velocity.magnitude);
        StateMachine.Update();
    }

    void SetToDeathState()
    {
        NavMeshAgent.enabled = false;
        StateMachine.ChangeState(AIStateID.Death);
    }
}
}