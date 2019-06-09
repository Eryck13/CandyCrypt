///////////////////////////////////////////////////////////////////////////////////////////
// Checks to see what button you are pressing to attack
// Using 'I', 'J', 'K', and 'L' (Diagonals too!)
///////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{ 
    // AHB = AttackHitBox (Any time you see AHB)
    private GameObject AHB;
    private GameObject player;
    private Animator Animation;

    ///////////////////////////////////////////////////////////////////////////////////////////
    // checks 0.3 left and right and 0.4 up and down 
    // Subject to change
    ///////////////////////////////////////////////////////////////////////////////////////////
    public static readonly float LOC_UP = 0.4f;
    public static readonly float LOC_SIDE = 0.3f;

    // These are to make the AttackHitBox have its components
    BoxCollider2D AHitBoxCollider;
    AttackHitBox AHBScript;
    Rigidbody2D AHBRigid2D;
    GameObject AttackHitBox;

    public Sprite AttackSprite;

    public 

    void Awake()
    {
        //Gets Animator
        Animation = GetComponent<Animator>();

        // Makes a hitbox for the player, and adds all components to make it
        AttackHitBox = new GameObject();
        AttackHitBox.AddComponent<SpriteRenderer>();
        AttackHitBox.AddComponent<Animator>();
        AttackHitBox.GetComponent<SpriteRenderer>().sprite = AttackSprite;
        player = GameObject.FindGameObjectWithTag("Player");

        AttackHitBox.name = "AttackHitBox";
        AttackHitBox.tag = "AttackHitBox";

        AHBRigid2D = AttackHitBox.AddComponent<Rigidbody2D>();
        AHBRigid2D.gravityScale = 0;

        // Adds script to check to see if it hits an enemy
        AHBScript = AttackHitBox.AddComponent<AttackHitBox>();

        AHitBoxCollider = AttackHitBox.AddComponent<BoxCollider2D>();
        AHitBoxCollider.offset = new Vector2(0f, 0f);
        AHitBoxCollider.size = new Vector2(0.15f, 0.15f);

        AHB = AttackHitBox;
        AHB.transform.parent = player.transform;
    }

    void Update()
    {

        if (Input.GetButtonDown("AttackUp"))
        {
            AHitBoxCollider.enabled = true;
            AttackHitBox.active = true;
            AudioManager.instance.Play("Attack");

            if (Input.GetButtonDown("AttackLeft"))
            {
                // Up Left
                AHB.gameObject.transform.position = new Vector3(this.transform.position.x - LOC_SIDE, this.transform.position.y + LOC_UP, this.transform.position.z);
            }
            else if (Input.GetButtonDown("AttackRight"))
            {
                // Up Right
                AHB.gameObject.transform.position = new Vector3(this.transform.position.x + LOC_SIDE, this.transform.position.y + LOC_UP, this.transform.position.z);
            }
            else
            {
                // Up
                AHB.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + LOC_UP, this.transform.position.z);
            }

            Animation.SetTrigger("Attack");
        }
        else if (Input.GetButtonDown("AttackDown"))
        {
            AHitBoxCollider.enabled = true;
            AttackHitBox.active = true;
            AudioManager.instance.Play("Attack");

            if (Input.GetButtonDown("AttackLeft"))
            {
                // Down Left
                AHB.gameObject.transform.position = new Vector3(this.transform.position.x - LOC_SIDE, this.transform.position.y - LOC_UP, this.transform.position.z);
            }
            else if (Input.GetButtonDown("AttackRight"))
            {
                // Down Right
                AHB.gameObject.transform.position = new Vector3(this.transform.position.x + LOC_SIDE, this.transform.position.y - LOC_UP, this.transform.position.z);
            }
            else
            {
                // Down
                AHB.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - LOC_UP, this.transform.position.z);
            }
            Animation.SetTrigger("Attack");

        }
        else if (Input.GetButtonDown("AttackLeft"))
        {
            AHitBoxCollider.enabled = true;
            AttackHitBox.active = true;
            AudioManager.instance.Play("Attack");

            // Left
            AHB.gameObject.transform.position = new Vector3(this.transform.position.x - LOC_SIDE, this.transform.position.y, this.transform.position.z);
            Animation.SetTrigger("Attack");
        }
        else if (Input.GetButtonDown("AttackRight"))
        {
            AHitBoxCollider.enabled = true;
            AttackHitBox.active = true;
            AudioManager.instance.Play("Attack");

            // Right
            AHB.gameObject.transform.position = new Vector3(this.transform.position.x + LOC_SIDE, this.transform.position.y, this.transform.position.z);
            Animation.SetTrigger("Attack");
        }

        // Disables after you are not pressing the button.
        if(!(Input.GetButtonDown("AttackUp") || Input.GetButtonDown("AttackLeft") || Input.GetButtonDown("AttackDown") || Input.GetButtonDown("AttackRight")))/*
            || !(
            || Input.GetButtonDown("AttackLeft")
            || Input.GetButtonDown("AttackDown")
            || Input.GetButtonDown("AttackRight")))*/
        {
            AHitBoxCollider.enabled = false;
            AttackHitBox.active = false;
        }
    }
}
