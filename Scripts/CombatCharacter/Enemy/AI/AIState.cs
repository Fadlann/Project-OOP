namespace CombatCharacter.Enemy.AI
{
public enum AIStateID
{
    ChasePlayer,
    Death,
    Idle,
    Shooting
}

public interface AIState 
{
    AIStateID GetID();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}
}