using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSlider : MonoBehaviour {

    public float levelSpeed = 2f;
    [HideInInspector] public bool playerDead;

    private void Start()
    {
        playerDead = false;
    }

    void Update()
    {
        if (!playerDead)
        {
            transform.position = new Vector3(transform.position.x + (levelSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }
}
