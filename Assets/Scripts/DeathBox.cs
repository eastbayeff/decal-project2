using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour {

    private LayerMask checkpointLayer;

    private void Awake()
    {
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player dies if contact
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<PlayerController>().GameOver();

        // checkpoint set if contact
        if (other.gameObject.layer == checkpointLayer)
        {
            other.gameObject.GetComponent<Checkpoint>().CheckpointReached();
        }

    }
}
