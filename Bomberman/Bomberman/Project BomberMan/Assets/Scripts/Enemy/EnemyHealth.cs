using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    public GameObject lootDrop;  
    public int health = 1;
    private int lootNo;
    public Transform localPos;


    public void TakeDamage(int damage)
    {
        
        health -= damage;
        AudioManager.instance.Play("EnemyHurt");

        if (health <= 0)
        {
            GameObject UI = GameObject.Find("BaseUI");
            UI.GetComponent<HudItems>().KillCounter += 1;
            DetectEnemies.instance.currentEnemies.Remove(this.gameObject);      // removes the enemy for the list of current enemies
            localPos = this.gameObject.transform;

            Destroy(this.gameObject);
        }
    } 
}
