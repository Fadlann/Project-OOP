using UnityEngine;
using Manager;

public class FootStepPlayer : MonoBehaviour 
{
    [SerializeField] AudioClip[] sounds;
    [SerializeField] Transform leftSource;
    [SerializeField] Transform rightSource;
    int curSound = -1;

    public void LeftStep()
    {
        AudioManager.Instance.PlaySound(sounds[(curSound + 1) % sounds.Length], leftSource.position);
    }

    public void RightStep()
    {
        AudioManager.Instance.PlaySound(sounds[(curSound + 1) % sounds.Length], rightSource.position);
    }
}