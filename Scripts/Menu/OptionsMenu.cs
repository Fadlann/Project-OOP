using UnityEngine;
using Manager;

namespace Menu
{
public class OptionsMenu : MonoBehaviour 
{
    public void SetMasterVolume(float value)
    {
		AudioManager.Instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetBGMVolume(float value)
    {
		AudioManager.Instance.SetVolume(value, AudioManager.AudioChannel.BGM);
    }

    public void SetSFXVolume(float value)
    {
		AudioManager.Instance.SetVolume(value, AudioManager.AudioChannel.SFX);
    }
}
}