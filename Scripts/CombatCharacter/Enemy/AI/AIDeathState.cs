namespace CombatCharacter.Enemy.AI
{
public class AIDeathState : AIState
{
    AIStateID AIState.GetID()
    {
        return AIStateID.Death;
    }

    void AIState.Enter(AIAgent agent)
    {   
        agent.SkinnedMeshRenderer.updateWhenOffscreen = true;
    }

    void AIState.Exit(AIAgent agent){ }

    void AIState.Update(AIAgent agent){ }
}
}