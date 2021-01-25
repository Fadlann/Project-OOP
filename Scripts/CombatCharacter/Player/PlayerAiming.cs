using UnityEngine;

namespace CombatCharacter.Player
{
public class PlayerAiming : MonoBehaviour
{
    private void Start() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;  
    }
}
}