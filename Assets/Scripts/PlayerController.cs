using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MenuController menuController;
    public CheckpointController checkpointController;


    [Header("Horizontal movement attributes")]
    public float horizontalAcceleration = 2f;
    public float returnNeutralSpeed = 1.5f;
    public float directionChangeLimiter = 0.5f;
    public float maxSpeed = 2f;

    [Header("Jump attributes")]
    public float initialJumpVelocity = 7f;

    [Tooltip("How fast after letting go of jump does player fall to ground")]
    public float jumpFallMultiplier = 2.5f;
    [Tooltip("Faster deceleration if not holding jump = snappy jump")]
    public float lowJumpMultiplier = 2f;

    [HideInInspector]
    public bool canJump;
    [HideInInspector]
    public bool isJumping;

    Rigidbody2D rb;

    [Header("For death")]
    public AudioSource audioSource;
    public GameObject slidingScreen;
    public GameObject deathText;
    public int deathCount;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        canJump = true;
        isJumping = false;
        deathCount = 0;
        deathText.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            checkpointController.LoadLast();
        }

        MoveHorizontal();

        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, initialJumpVelocity);
        }

        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpFallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        
    }

    void MoveHorizontal()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        // moving right, holding right || moving left, holding left
        if ((horizontal > 0 && rb.velocity.x > 0) || (horizontal < 0 && rb.velocity.x < 0))
        {
            rb.AddForce(new Vector2(horizontalAcceleration * horizontal, 0), ForceMode2D.Impulse);
        }

        // holding right, moving left || holding left, moving right
        if ((horizontal > 0 && rb.velocity.x <= 0) || (horizontal < 0 && rb.velocity.x >= 0))
        {
            rb.AddForce(new Vector2(horizontalAcceleration * directionChangeLimiter * horizontal, 0), ForceMode2D.Impulse);
        }

        if (rb.velocity.x > maxSpeed)
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        if (rb.velocity.x < -maxSpeed)
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);


        if (horizontal == 0)
            rb.velocity = new Vector2(rb.velocity.x - (returnNeutralSpeed * Time.deltaTime), rb.velocity.y);

    }


    public void GameOver()
    {
        deathCount++;
        AudioController.Instance.StopMusic();
        slidingScreen.GetComponent<ScreenSlider>().playerDead = true;
        audioSource.Play();
        deathText.SetActive(true);
        Debug.Log("game over");
    }

    public void Victory()
    {
        menuController.Victory(deathCount);
        enabled = false;
    }
}