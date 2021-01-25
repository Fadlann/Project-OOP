using UnityEngine;

namespace CombatCharacter.Player
{
public class PlayerMovement : MonoBehaviour
{
    [Header("For non aiming movement")]
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float turnSpeed = 15;

    private Animator animator;
    private Vector3 input = Vector3.zero;
    private bool disabled = false;

    private PlayerWeapon weaponHandler;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        GetComponent<Player>().OnDeath += DisableMovement;
        weaponHandler = GetComponent<PlayerWeapon>();
    }

    void Update()
    {
        if (disabled)
            return;
        
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.z);
        animator.SetFloat("Magnitude", input.magnitude);

        if (weaponHandler && !weaponHandler.Armless) 
            AimingMovement();
        else
            NonAimingMovement();
    }

    private void DisableMovement()
    {
        disabled = true;
    }

    private void NonAimingMovement()
    {
        input = Camera.main.transform.TransformDirection(input);
        input = input - Vector3.up * input.y;

        Quaternion originalRot = transform.rotation;
        transform.LookAt(transform.position + input);
        Quaternion newRot = transform.rotation;
        transform.rotation = originalRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
    }

    private void AimingMovement()
    {
        float yawCamera = Camera.main.transform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }
}
}