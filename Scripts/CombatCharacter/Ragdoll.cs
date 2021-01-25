using UnityEngine;
using System.Linq;

namespace CombatCharacter
{
public class Ragdoll : MonoBehaviour 
{
    Rigidbody[] rigidbodies;
    Animator anim;

    private void Start() 
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        GetComponent<CombatEntity>().OnDeath += ActivateRagdoll;
        DeactivateRagdoll();
    }    

    public void ActivateRagdoll()
    {
        foreach(var rigidbody in rigidbodies.Skip(1))
        {
            rigidbody.isKinematic = false;
            rigidbody.GetComponent<Collider>().isTrigger = false;
        }
        anim.enabled = false;
    }

    public void DeactivateRagdoll()
    {
        foreach(var rigidbody in rigidbodies.Skip(1))
        {
            rigidbody.isKinematic = true;
            rigidbody.GetComponent<Collider>().isTrigger = true;
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