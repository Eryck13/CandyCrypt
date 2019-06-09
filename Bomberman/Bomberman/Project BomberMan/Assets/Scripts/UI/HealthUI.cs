using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour
{
    private const int num_hitBoxes  = 9;

    private int hdiff   = 0;    // Health Difference

  
    public int health  = 9;
    //public GameObject stats;

    [SerializeField]
    private int armor   = 0;

    private int adiff   = 0;    // Armor difference

    [SerializeField]
    private GameObject[] lives;

    [SerializeField]
    private Image[] hp;

    [SerializeField]
    private Image[] armors;


    public static readonly int NUM_HEALTH       = 3;    // Start with 3 heart at the beginning
    public static readonly int HITS_PER_LIFE    = 3;    // Hitbox per life(same for health and armor for now)


    private void Update()
    {
        health = this.GetComponent<BaseStats>().GetTotalModdedHealth();
        armor = this.GetComponent<BaseStats>().GetTotalModdedDefense();
        if (health != hdiff || armor != adiff)
        {
            // dummyproofing
            health = checkLimit(health);
            armor  = checkLimit(armor);

            int healthCheck = health;
            int armorcheck  = armor;

            // do the loop
            loop(healthCheck, armorcheck);

            hdiff = num_hitBoxes;
            adiff = num_hitBoxes;
        }
    }

    float getfill(int HC)
    {

        float retVal = 1.0f;

        if (HC <= HITS_PER_LIFE)
        {
            retVal = (float)HC / 3.0f;
        }

        if (retVal <= 0)
        {
            retVal = 0.0f;
        }
        
        return retVal;
    }

    int checkLimit(int value)
    {
        if (value > num_hitBoxes)
        {
            value = 9;
        } else if (value < 0)
        {
            value = 0;
        }
        return value;
    }

    void loop(int healthCheck, int armorcheck)
    {
        // loop update the armor and health sprites

        for (int i = 0; i < NUM_HEALTH; i++)
        {
            if (lives[i] != null)
            {
                if (healthCheck > 0)
                {
                    lives[i].SetActive(true);
                }

                if (armorcheck > 0)
                {
                    lives[i + NUM_HEALTH].SetActive(true);
                }

                hp[i].fillAmount = getfill(healthCheck);
                armors[i].fillAmount = getfill(armorcheck);

                if (getfill(healthCheck) == 0.0f)
                {
                    lives[i].SetActive(false);
                }

                if (getfill(armorcheck) == 0.0f)
                {
                    lives[i + NUM_HEALTH].SetActive(false);
                }

                healthCheck -= HITS_PER_LIFE;
                armorcheck -= HITS_PER_LIFE;
            }
        }
    }

}



