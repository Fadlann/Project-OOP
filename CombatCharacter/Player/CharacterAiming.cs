using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;
using CombatCharacter.Player.Weaponry;

namespace CombatCharacter.Player
{
public class CharacterAiming : MonoBehaviour
{
    [SerializeField] float turnSpeed = 15;
    [SerializeField] float aimDuration = .3f;

    ActiveWeapon weapon;
    CharacterLocomotion movement;

    private void Start() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;  

        weapon = GetComponent<ActiveWeapon>();
        movement = GetComponent<CharacterLocomotion>();
    }

    void FixedUpdate()
    {
        if (!weapon || !movement || (weapon.Aiming))
        {
            float yawCamera = Camera.main.transform.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // rotation change
            Vector3 input = Camera.main.transform.TransformDirection(movement.InputM); // idk what is this
            input = input - Vector3.up * input.y;

            Quaternion originalRot = transform.rotation; // set the original rotation
            transform.LookAt(transform.position + input); // make the transform too look at the direction input
            Quaternion newRot = transform.rotation; // set the new rot that that direction input
            transform.rotation = originalRot; // set the transform rotation to the original rotation 
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, movement.RotateSpeed * Time.deltaTime); // lerp
            // idk how that works
        }
    }
}
}