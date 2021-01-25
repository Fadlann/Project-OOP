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
            agent.ActiveWeapon.EquipWeapon();
        }

        public void Update(AIAgent agent)
        {
            agent.transform.LookAt(agent.PlayerTransform);
            
            agent.ActiveWeapon.UpdateAttack();
            
            Vector3 dirToTarget = (agent.PlayerTransform.position - agent.transform.position).normalized;

            RaycastHit hit;
            if(Physics.Raycast(agent.transform.position + Vector3.up * .25f, dirToTarget, out hit))
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