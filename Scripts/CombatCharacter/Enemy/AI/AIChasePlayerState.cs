using UnityEngine;
using UnityEngine.AI;

namespace CombatCharacter.Enemy.AI
{
public class AIChasePlayerState : AIState
{
    private float timer = .0f;

    public void Enter(AIAgent agent){ }

    public void Exit(AIAgent agent){ }

    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;
    }

    public void Update(AIAgent agent)
    {
        if (!agent.enabled)
            return;

        Vector3 dirToTarget = (agent.PlayerTransform.position - agent.transform.position).normalized;

        RaycastHit hit;
        if(Physics.Raycast(agent.transform.position + Vector3.up, dirToTarget, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                agent.NavMeshAgent.isStopped = true;
                agent.StateMachine.ChangeState(AIStateID.Shooting);
                return;
            }
            else
                agent.NavMeshAgent.isStopped = false;
        }

        if (!agent.NavMeshAgent.hasPath)
            agent.NavMeshAgent.destination = agent.PlayerTransform.position;
        
        timer -= Time.deltaTime;
        if(timer < .0f)
        {
            Vector3 dir = (agent.PlayerTransform.position - agent.NavMeshAgent.destination);
            dir.y = 0;
            if(dir.sqrMagnitude > agent.Config.MaxDistance * agent.Config.MaxDistance 
                && agent.NavMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
            {
                agent.NavMeshAgent.destination = agent.PlayerTransform.position;
            }
            timer = agent.Config.MaxTime;
        }
    }
}
}