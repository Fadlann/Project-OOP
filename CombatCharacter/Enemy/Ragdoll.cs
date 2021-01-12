using UnityEngine;

namespace CombatCharacter.Enemy
{
public class Ragdoll : MonoBehaviour 
{
    Rigidbody[] rigidbodies;
    Animator anim;

    public Rigidbody[] Rigidbodies{get{return rigidbodies;}}

    private void Start() 
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        DeactivateRagdoll();
    }    

    public void ActivateRagdoll()
    {
        foreach(var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        anim.enabled = false;
    }

    public void DeactivateRagdoll()
    {
        foreach(var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        anim.enabled = true;
    }

    public void ApplyForce(Vector3 force)
    {
        var rigidbody = anim.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
}