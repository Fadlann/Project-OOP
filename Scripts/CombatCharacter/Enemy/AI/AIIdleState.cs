using UnityEngine;

namespace CombatCharacter.Enemy.AI
{
public class AIIdleState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.Idle;
    }

    public void Enter(AIAgent agent){ }

    public void Exit(AIAgent agent){ }

    public void Update(AIAgent agent)
    {
        Vector3 playerDirection = agent.PlayerTransform.position - agent.transform.position;

        if (playerDirection.magnitude > agent.Config.MaxSightDistance)
            return;

        Vector3 agentDirection = agent.transform.forward;

        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if(dotProduct > 0.0f)
            agent.StateMachine.ChangeState(AIStateID.ChasePlayer);
    }
}
}