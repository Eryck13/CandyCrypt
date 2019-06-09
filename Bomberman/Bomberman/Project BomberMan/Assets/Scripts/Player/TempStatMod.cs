using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempStatMod : MonoBehaviour
{
    private int attack = 0;
    private int speed = 0;
    private bool isSodaTime = false;
    private bool resetSoda = false;

    private int sodaAttack = 2;
    private int sodaSpeed = 1;

    private HudItems Powerup;

    private void Awake()
    {
        Powerup = FindObjectOfType<HudItems>();
    }
    private void FixedUpdate()
    {

        if(Powerup != null && Powerup.bPoweredUp == true)
        {
            attack = 2;
            speed = 1;
        }
        else if (Powerup != null && Powerup.bPoweredUp == false)
        {
            attack = 0;
            speed = 0;
        }
    }
    public IEnumerator SodaPower()
    {
        /*float duration = 10f;

        float timeStamp = Time.time;

        attack += sodaAttack;
        speed += sodaSpeed;

        while (Time.time < timeStamp + duration)
        {
            if (resetSoda)
            {
                resetSoda = false;

                timeStamp = Time.time;
            }

        }

        attack = attack - sodaAttack;
        speed = speed - sodaSpeed;*/


        attack += sodaAttack;
        speed += sodaSpeed;
        yield return new WaitForSeconds(10);
        attack -= sodaAttack;
        speed -= sodaSpeed;



    }

    public int GetTempAttackMod()
    {
        return attack;
    }

    public int GetTempSpeedMod()
    {
        return speed;
    }
}
