using UnityEngine;
using System.Collections;

namespace Manager
{
public class AudioManager : MonoBehaviour 
{
    [SerializeField, Range(0, 1)] private float masterVolumePercent;
    [SerializeField, Range(0, 1)] private float sfxVolumePercent;
    [SerializeField, Range(0, 1)] private float bgmVolumePercent;

    private int activeBGMIndex;
    private AudioSource[] bgmSources;

    public float MasterVolumePercent { get {return masterVolumePercent;} }
	public float SfxVolumePercent { get {return sfxVolumePercent;} }
	public float BackgroundVolumePercent { get {return bgmVolumePercent;} }

	public enum AudioChannel {Master, SFX, BGM};

    // Singleton
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameObject("AudioManager").AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    private void Awake() 
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;    
        DontDestroyOnLoad(gameObject);

        bgmSources = new AudioSource[2];

        for (int i = 0; i < 2; i++) {
            GameObject newBGMSource = new GameObject ("Music source " + (i + 1));
            bgmSources[i] = newBGMSource.AddComponent<AudioSource> ();
            newBGMSource.transform.parent = transform;
            bgmSources[i].priority = 0; // highest priority
        }

    }

    public void PlaySound(AudioClip clip, Vector3 pos) {
		if (clip != null) {
			AudioSource.PlayClipAtPoint (clip, pos, sfxVolumePercent * masterVolumePercent);
		}
	} 

    public void PlayBGM(AudioClip clip, float fadeDuration = 1) {
		activeBGMIndex = 1 - activeBGMIndex;
		bgmSources [activeBGMIndex].clip = clip;
		bgmSources [activeBGMIndex].Play ();

		StartCoroutine(AnimateMusicCrossfade(fadeDuration));
	}

    IEnumerator AnimateMusicCrossfade(float duration) {
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * 1 / duration;
			bgmSources [activeBGMIndex].volume = Mathf.Lerp (0, bgmVolumePercent * masterVolumePercent, percent);
			bgmSources [1-activeBGMIndex].volume = Mathf.Lerp (bgmVolumePercent * masterVolumePercent, 0, percent);
			yield return null;
		}
	}

    public void SetVolume(float volumePercent, AudioChannel channel) {
		switch (channel) {
		case AudioChannel.Master:
			masterVolumePercent = volumePercent;
			break;
		case AudioChannel.SFX:
			sfxVolumePercent = volumePercent;
			break;
		case AudioChannel.BGM:
			bgmVolumePercent = volumePercent;
			break;
		}

		bgmSources [0].volume = bgmVolumePercent * masterVolumePercent;
		bgmSources [1].volume = bgmVolumePercent * masterVolumePercent;

		PlayerPrefs.SetFloat ("master vol", masterVolumePercent);
		PlayerPrefs.SetFloat ("sfx vol", sfxVolumePercent);
		PlayerPrefs.SetFloat ("music vol", bgmVolumePercent);
		PlayerPrefs.Save ();
	}
}
}