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
            agent.ActiveWeapon.Weapon.StartAttack();
        }

        public void Update(AIAgent agent)
        {
            agent.transform.LookAt(agent.PlayerTransform);
            
            if(agent.ActiveWeapon.Weapon.IsAttacking)
                agent.ActiveWeapon.Weapon.UpdateAttack();
            
            Vector3 dirToTarget = (agent.PlayerTransform.position - agent.transform.position).normalized;

            RaycastHit hit;
            if(Physics.Raycast(agent.transform.position + Vector3.up, dirToTarget, out hit))
            {
                if (hit.transform.tag != "Player")
                {
                    agent.StateMachine.ChangeState(AIStateID.ChasePlayer);
                }
            }
        }

        public void Exit(AIAgent agent)
        {
            agent.ActiveWeapon.Weapon.StopAttack();
        }
    }
}