using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickups : MonoBehaviour
{
    private GameObject objectInCollider;
    public GameObject pickupPromt;
    public GameObject BaseUI;
    public int increaseValueBy;

    private void Awake()
    {
        pickupPromt = GameObject.Find("Pickup Prompt");
        BaseUI = GameObject.Find("BaseUI");
    }

    private void FixedUpdate()
    {
        // makes it so the palyer needs to interact to with pickups that are not Health.
        // done through checking the tag of the object in the collider and the type of pickup
        if (objectInCollider != null)
        {
            //pickupPromt.GetComponent<Text>().enabled = true;
            if ((Input.GetButtonDown("Pickup") || Input.GetAxis("Pickup") > 0) && objectInCollider.tag == "Player" &&
                this.tag != "Health_Pickup" && this.tag != "Powerup_Pickup")
            {
                switch (this.tag)
                {
                    case "Armor_Pickup":
                        AudioManager.instance.Play("Health");

                        objectInCollider.GetComponent<BaseStats>().AddArmorDefense(increaseValueBy);
                        break;
                    case "Attack_Pickup":
                        AudioManager.instance.Play("Health");

                        objectInCollider.GetComponent<PermStatMod>().SetPermAttackMod(increaseValueBy);
                        break;
                    case "Helmet_Pickup":
                        AudioManager.instance.Play("Health");

                        objectInCollider.GetComponent<BaseStats>().AddHelmetDefense(increaseValueBy);
                        break;
                    default:
                        Debug.Log("Pickup doesn't have a valid tag");
                        break;
                }
                //pickupPromt.GetComponent<Text>().enabled = false;
                Destroy(gameObject);
            }
        }
        else
        {
            //pickupPromt.GetComponent<Text>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // displays the pickup prompt for nonhealth pickups and recordes the object in the collider if it is the player
        if (other.tag == "Player" && this.tag != "Health_Pickup" && this.tag != "Powerup_Pickup")
        {
            pickupPromt.GetComponent<Text>().enabled = true;
            objectInCollider = other.gameObject;
        }

        // auto pickups the health based pickups and adds thier effect
        if (other.tag == "Player" && (this.tag == "Health_Pickup" || this.tag == "Powerup_Pickup"))
        {
            switch(this.tag)
            {
                case "Health_Pickup":
                    AudioManager.instance.Play("Health");
                    other.GetComponent<BaseStats>().IncreaseHealth(increaseValueBy);
                    break;
                case "Powerup_Pickup":
                    AudioManager.instance.Play("Coke");
                    BaseUI.GetComponent<HudItems>().bPoweredUp = true;
                    // TALK WITH MASON TO IMPLEMENT
                    break;
                default:
                    Debug.Log("Pickup doesn't have a valid tag");
                    break;
            }
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        // disable pickup prompt when player object leaves the collider
        if (collision.tag == "Player")
        {
            objectInCollider = null;
            pickupPromt.GetComponent<Text>().enabled = false;

        }

    }
}
