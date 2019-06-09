using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int           HP = 5;
    private const float         Spd = 5;
    private const float         Atk = 5;

    // attributes which can be used to save file later
    public int health;
    public int level;
    public int score;      //score for saving data

    private float speed;
    private float speedMod = 0;
    
    private float attackDmg;
    private float attackDmgMod = 0;// int?
    


    [SerializeField]
    private GameObject maincamera;
    private int increaseValueBy;

    //[SerializeField]
    //private GameObject player;

    void cokedUp()
    {
        //Increases the player's movement and attack speed
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.GetComponent<Pickups>().PickupStatUpdate(gameObject.tag, increaseValueBy);
        
    }


    private void Start()
    {
        health = HP;
        speed = Spd;
        attackDmg = Atk;
    }

    int getHealth()
    {
        return health;
    }

    float getSpeed()
    {
        return speed;
    }

    float getattack()
    {
        return attackDmg;
    }

    void setAttack(float AD)
    {
        attackDmg = AD;
    }

   void takeDamage(int damage)
    {
        health -= damage;
    }

    float DealDamage()
    {
        return attackDmg + attackDmgMod;
    }

    void DamageMod(float Dmg)
    {
        attackDmgMod = Dmg;
    }



    public void SavePLayer()
    {
        Save.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = Save.LoadPlayer();

        // add the attributes to be loaded here
        level = data.level;
        health = data.health;
        score = data.score;

    }
}
