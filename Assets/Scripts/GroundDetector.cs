using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private LayerMask groundLayer;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == groundLayer)
        {
            playerController.canJump = true;
            playerController.isJumping = false;
            // Debug.Log("on enter, canJump = " + playerController.canJump + ", isJumping = " + playerController.isJumping);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // still check for floor; don't want walls resetting jump
        if (other.gameObject.layer == groundLayer)
        {
            playerController.canJump = false;
            playerController.isJumping = true;
            // Debug.Log("on exit, canJump = " + playerController.canJump + ", isJumping = " + playerController.isJumping);
        }

    }

}

