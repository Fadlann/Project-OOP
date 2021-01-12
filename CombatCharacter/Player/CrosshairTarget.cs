using UnityEngine;

namespace CombatCharacter.Player
{
public class CrosshairTarget : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        ray.origin = Camera.main.transform.position;
        ray.direction = Camera.main.transform.forward;

        if (Physics.Raycast(ray, out hit)) 
        {
            transform.position = hit.point;
        } 
        else 
        {
            transform.position = ray.origin + ray.direction * 1000.0f;
        }
    }
}
}