using UnityEngine;
using UnityEngine.AI;

namespace CombatCharacter.Enemy.AI
{
public class AIChasePlayerState : AIState
{
    float timer = .0f;

    public void Enter(AIAgent agent)
    {

    }

    public void Exit(AIAgent agent)
    {
    }

    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;
    }

    public void Update(AIAgent agent)
    {
        if (!agent.enabled)
            return;

        Vector3 dirToTarget = (agent.playerTransform.position - agent.transform.position).normalized;

        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position - dirToTarget * agent.config.MaxDistToTarget;
        }
        
        timer -= Time.deltaTime;
        if(timer < .0f)
        {
            Vector3 dir = (agent.playerTransform.position - agent.navMeshAgent.destination);
            dir.y = 0;
            if(dir.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance && agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
            {
                agent.navMeshAgent.destination = agent.playerTransform.position - dirToTarget * agent.config.MaxDistToTarget;
            }
            timer = agent.config.maxTime;
        }
    }
}
}