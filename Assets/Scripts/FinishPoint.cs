using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private GameObject playerObject;

    private void Awake()
    {
        playerObject = GameObject.Find("Player");

        if (playerObject == null)
            Debug.LogWarning("Finish point cannot find a player object. Is it named correctly?");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == playerObject.tag)
            other.GetComponent<PlayerController>().Victory();
    }

}

