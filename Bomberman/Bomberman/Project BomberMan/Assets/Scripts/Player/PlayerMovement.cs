/***********************************************************************************************************************************************
INSTRUCTIONS TO USE THIS SCRIPT - Add a 2d rigibody to the player and add the script
Scipt Functionality - Movement, camera following, colliding (add box2d collider)
also put box collidor on the walls of the left for camera to follow the camera. 
Parent the player to the camera, also attach the cameraOffset script to the camera if you player object has a cirle collider
************************************************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D     rb2d;
    private Vector2         moveVelocity;
    private SpriteRenderer  Sprite;         
    private Animator        Animation;

    public float speed;

    private void Start()
    {
        rb2d        = GetComponent <Rigidbody2D>();
        Sprite      = GetComponent <SpriteRenderer>();  
        Animation   = GetComponent <Animator>();
    }

    private void Update()
    {
        // using Input. GetAxisRaw so that it stops whenever we are not pressing any key 
        float moveHorizontal    = Input.GetAxisRaw ("Horizontal");
        float moveVertical      = Input.GetAxisRaw ("Vertical");

        Vector2 movement        = new Vector2(moveHorizontal, moveVertical);
        moveVelocity = movement.normalized * this.GetComponent<BaseStats>().GetTotalModdedSpeed();

        // Checks direction
        if (moveHorizontal > 0)
        {
            Sprite.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            Sprite.flipX = true;
        }

        var player  = GetComponent<PlayerCollision>();
        speed       = player.speed;

        // Checks if Moving
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            Animation.SetBool("Walking", true);
        }
        else
        {
            Animation.SetBool("Walking", false);
        }

    }
    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
    }
}
