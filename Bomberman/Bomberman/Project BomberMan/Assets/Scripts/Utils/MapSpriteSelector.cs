///////////////////////////////////////////////////////////////////////////////////////////
// This uses 15 sprites to determine the rotation of the room and how many doors it has.
// The long nested if statements was the best way I see fit, if you can think of another
// way to do this let me know.(Danny)
// Also it determines what color the room is by the type. This will change when I add the 
// new sprites.
//**Needs sprite renderer** **All 15 room sprites**
///////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpriteSelector : MonoBehaviour
{

    /*public Sprite spU, spD, spR, spL,
            spUD, spRL, spUR, spUL, spDR, spDL,
            spULD, spRUL, spDRU, spLDR, spUDRL;
            */
   // const int MAX_DOORS = 4;

  //  public Sprite[] Doors = new Sprite[4];

  //  public bool[] DoorSet = new bool[4];

//    public bool up, down, left, right;
   /* public int type; // 0: normal, 1: enter, 2: exit
    public bool end = false;*/
    public Color normalColor, enterColor, endColor;//1, endColor2, endColor3;
    Color mainColor;
    SpriteRenderer rend;
    void Start()
    {

    }

    void Populate()
    {
        rend = GetComponent<SpriteRenderer>();
        //mainColor = normalColor;
       // PickSprite();
       // CheckEnd();
        //PickColor();
    }

  /*  void CheckEnd()
    {
        // int bossType;
        /*        if (up && !down && !left && !right)
                {
                    type = 2; //bossType = Random.Range(2, 4);
                }
                if (!up && down && !left && !right)
                {
                    type = 2;//bossType = Random.Range(2, 4);
                }
                if (!up && !down && left && !right)
                {
                    type = 2;//bossType = Random.Range(2, 4);
                }
                if (!up && !down && !left && right)
                {
                    type = 2;//bossType = Random.Range(2, 4);
                }
          
        int numDoors = 0;
        for (int i = 0; i < MAX_DOORS; i++)
        {
            if (DoorSet[i])
            {
                numDoors++;
            }
        }

        if (numDoors == 1)
        {
            type = 2;
        }
    }
    */

   /* void PickSprite()
    {

        for (int i = 0; i < MAX_DOORS; i++)
        {
            if (DoorSet[i])
            {

            }
        }
        //picks correct sprite based on the four door bools

        /*if (up && down && right && left)
        {

        }
        if (up && down && right && !left)
        {

        }
        if (up && down && !right && left)
        {

        }
        if (up && !down && right && left)
        {

        }
        if (!up && down && right && left)
        {

        }

        if (up)
        {
            if (down)
            {
                if (right)
                {
                    if (left)
                    {
                        rend.sprite = spUDRL;
                    }
                    else
                    {
                        rend.sprite = spDRU;
                    }
                }
                else if (left)
                {
                    rend.sprite = spULD;
                }
                else
                {
                    rend.sprite = spUD;
                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        rend.sprite = spRUL;
                    }
                    else
                    {
                        rend.sprite = spUR;
                    }
                }
                else if (left)
                {
                    rend.sprite = spUL;
                }
                else
                {
                    rend.sprite = spU;
                }
            }
            return;
        }
        if (down)
        {
            if (right)
            {
                if (left)
                {
                    rend.sprite = spLDR;
                }
                else
                {
                    rend.sprite = spDR;
                }
            }
            else if (left)
            {
                rend.sprite = spDL;
            }
            else
            {
                rend.sprite = spD;
            }
            return;
        }
        if (right)
        {
            if (left)
            {
                rend.sprite = spRL;
            }
            else
            {
                rend.sprite = spR;
            }
        }
        else
        {
            rend.sprite = spL;
        }
        

    }*/
    /*
    void PickColor()
    { //changes color based on what type the room is
        if (type == 0)
        {
            mainColor = normalColor;
        }
        else if (type == 1)
        {
            mainColor = enterColor;
        }
        else if (type == 2)
        {
            mainColor = endColor;
        }
        rend.color = mainColor;
    }*/
}