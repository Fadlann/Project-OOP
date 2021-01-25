using UnityEngine;

namespace CombatCharacter.Enemy.AI
{
    public class AIShootingState : AIState
    {
        public AIStateID GetID()
        {
            return AIStateID.Shooting;
        }

        public void Enter(AIAgent agent)
        {
            agent.ActiveWeapon.Attack();
        }

        public void Update(AIAgent agent)
        {
            agent.transform.LookAt(agent.PlayerTransform);
            
            agent.ActiveWeapon.UpdateAttack();
            
            Vector3 dirToTarget = (agent.PlayerTransform.position - agent.transform.position).normalized;

            RaycastHit hit;
            if(Physics.Raycast(agent.transform.position, dirToTarget, out hit, 1000, ~LayerMask.GetMask("Enemy")))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.tag != "Player")
                {
                    agent.StateMachine.ChangeState(AIStateID.ChasePlayer);
                }
            }
        }

        public void Exit(AIAgent agent)
        {
            agent.ActiveWeapon.StopAttack();
        }
    }
}