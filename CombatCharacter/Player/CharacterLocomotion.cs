using UnityEngine;

namespace CombatCharacter.Player
{
public class CharacterLocomotion : MonoBehaviour
{
    [Header("For non aiming movement")]
    [SerializeField] float rotateSpeed = 10f;

    Animator animator;

    Vector3 input = Vector3.zero;

    public Vector3 InputM {get{return input;}}
    public float RotateSpeed {get{return rotateSpeed;}}

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.z);
        animator.SetFloat("Magnitude", input.magnitude);
    }
}
}