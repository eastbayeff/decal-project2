using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private GameObject playerObject;
    private CheckpointController checkpointController;

    private void Awake()
    {
        playerObject = GameObject.Find("Player");
        checkpointController = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();

        if (playerObject == null || checkpointController == null)
            Debug.LogWarning("Checkpoint cannot find valid Player or CheckpointController. Are these game objects in scene, with correct spelling?");
    }

    public void CheckpointReached()
    {
        Debug.Log("Checkpoint reached!");
        checkpointController.SaveData();
    }

}
