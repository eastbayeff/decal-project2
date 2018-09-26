using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public static AudioController Instance;

    private AudioSource musicPlayer;
    public float blipDuration = 0.5f;
    private float startingVolume;

    private float pauseMusicTime;

    private void Awake()
    {
        #region singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(Instance);
        #endregion

        musicPlayer = GetComponent<AudioSource>();
    }

    private void Start()
    {
        startingVolume = musicPlayer.volume;
    }

    public void BlipVolume()
    {
        // only blip if not already changed volume
        if (musicPlayer.volume == startingVolume)
        {
            musicPlayer.volume = 1;
            StartCoroutine(RestoreVolumeAfterTime(blipDuration));
        }
    }

    public void ToggleMusic(bool choice)
    {
        if (choice)
            PauseMusic();
        else if (!choice)
            UnpauseMusic();
    }

    private void PauseMusic()
    {
        pauseMusicTime = musicPlayer.time;
        musicPlayer.Stop();
    }

    private void UnpauseMusic()
    {
        musicPlayer.time = pauseMusicTime;
        musicPlayer.Play();
    }

    public void StopMusic()
    {
        musicPlayer.Stop();
    }

    // getters and settings to access current music clip's timestamp
    public float MusicTime
    {
        get { return musicPlayer.time; }
        set { musicPlayer.time = value; }
    }

    IEnumerator RestoreVolumeAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        musicPlayer.volume = startingVolume;
    }
}
