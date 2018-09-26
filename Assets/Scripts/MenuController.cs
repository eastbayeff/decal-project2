using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Tooltip("UI Image to display during pause")]
    public Image pauseImage;

    public Image victoryImage;
    public AudioClip victoryMusic;
    private Text victoryText;


    [HideInInspector]
    public bool isPaused;
    /*
    public AudioClip pauseSound;
    public AudioClip unpauseSound;
    */

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        victoryText = victoryImage.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        isPaused = false;
    }

    void Update()
    {
        // Pause menu button determined in project settings > input
        if (Input.GetButtonDown("PauseMenu"))
            PauseMenu();

    }

    // toggle the pause menu, disable the PlayerController while paused
    private void PauseMenu()
    {
        isPaused = !pauseImage.gameObject.activeSelf;
        pauseImage.gameObject.SetActive(isPaused);

        // Actually stop the game engine
        Time.timeScale = isPaused ? 0 : 1;

        // toggle the music on or off
        AudioController.Instance.ToggleMusic(isPaused);

        // TODO(Christian): implement sound 
        // SoundManager.Instance.PlaySingle(isPaused ? pauseSound : unpauseSound);
    }

    public void Victory(int deathCount)
    {
        victoryImage.gameObject.SetActive(true);
        Time.timeScale = 0;
        AudioController.Instance.StopMusic();
        audioSource.clip = victoryMusic;
        audioSource.Play();
        victoryText.text = deathCount.ToString();
    }
}

