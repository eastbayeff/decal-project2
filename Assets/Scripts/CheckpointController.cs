using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CheckpointData
{
    public float musicClipTime;
    public Vector3 playerPosition;
    public Vector3 playerVelocity;
    public Vector3 cameraPosition;
}

public class CheckpointController : MonoBehaviour
{

    public AudioController audioController;
    public GameObject playerObject;
    public GameObject slidingScreen;
    public GameObject deathText;
    public Transform cameraViewport;

    private CheckpointData lastCheckpoint;

    private void Start()
    {
        // first checkpoint is when starting
        lastCheckpoint = SaveData();
    }

    // called when OnTriggerEnter2D death box collider -> checkpoint object
    public CheckpointData SaveData()
    {
        lastCheckpoint = new CheckpointData();
        lastCheckpoint.musicClipTime = AudioController.Instance.MusicTime;
        lastCheckpoint.playerPosition = playerObject.transform.position;
        lastCheckpoint.playerVelocity = playerObject.GetComponent<Rigidbody2D>().velocity;
        lastCheckpoint.cameraPosition = cameraViewport.position;

        return lastCheckpoint;
    }

    // called from pushing "R" in playerController
    public void LoadLast()
    {
        LoadCheckpoint(lastCheckpoint);
    }

    private void LoadCheckpoint(CheckpointData data)
    {
        Debug.Log("restarting at last checkpoint!");
        AudioController.Instance.MusicTime = data.musicClipTime;
        AudioController.Instance.GetComponent<AudioSource>().Play();
        playerObject.transform.position = data.playerPosition;
        playerObject.GetComponent<Rigidbody2D>().velocity = data.playerVelocity;
        cameraViewport.position = data.cameraPosition;
        slidingScreen.GetComponent<ScreenSlider>().playerDead = false;
        deathText.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
