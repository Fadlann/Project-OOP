using UnityEngine;

namespace CombatCharacter.Player.Weaponry
{
public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] Vector2[] recoilPattern;
    [SerializeField] float duration = .1f;
    
    Cinemachine.CinemachineFreeLook playerCamera;
    Cinemachine.CinemachineImpulseSource cameraShake;
    Animator rigController;
    float time;
    int index;

    # region public variables
    public Cinemachine.CinemachineFreeLook PlayerCamera {get{return playerCamera;} set {playerCamera = value;}}
    public Animator RigController {get{return rigController;} set{rigController = value;}}
    # endregion

    private void Awake() 
    {
        cameraShake = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }

    public void GenerateRecoil(string weaponName)
    {
        time = duration;
        cameraShake.GenerateImpulse(Camera.main.transform.forward);
        index = (index + 1) % recoilPattern.Length;
        rigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);
    }

    private void Update() 
    {
        if(time > 0)    
        {
            playerCamera.m_XAxis.Value -= ((recoilPattern[index].x/10) * Time.deltaTime) / duration;
            playerCamera.m_YAxis.Value -= ((recoilPattern[index].y/1000) * Time.deltaTime) / duration;
            time -= Time.deltaTime;
        }
    }
}
}