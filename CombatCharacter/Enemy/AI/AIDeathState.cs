using UnityEngine;

namespace CombatCharacter.Enemy.AI
{
public class AIDeathState : AIState
{
    public Vector3 direction;

    AIStateID AIState.GetID()
    {
        return AIStateID.Death;
    }

    void AIState.Enter(AIAgent agent)
    {
        if(agent.ragdoll) 
        {
            agent.ragdoll.ActivateRagdoll(); 
            direction.y = 1;
            agent.ragdoll.ApplyForce(direction * agent.config.dieForce);
        }
        
        agent.healthBar.gameObject.SetActive(false);
        agent.skinnedMeshRenderer.updateWhenOffscreen = true;
    }

    void AIState.Exit(AIAgent agent)
    {
    }

    void AIState.Update(AIAgent agent)
    {
    }
}
}