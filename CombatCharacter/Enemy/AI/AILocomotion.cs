using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CombatCharacter.Enemy.AI
{
public class AILocomotion : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    Health health;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        if (health)
            health.OnDeath += DisableAI;
    }

    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    void DisableAI()
    {
        agent.enabled = false;
    }
}
}