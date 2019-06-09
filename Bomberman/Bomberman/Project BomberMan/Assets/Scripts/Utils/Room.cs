///////////////////////////////////////////////////////////////////////////////////////////
// This is just a room class, spawns in the correct doors using levelGeneration
// script, and sprites attached to it.
// Also could have up to four doors "top, bottom, left, right" with a minimum of 1.
///////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public static int MAX_DOORS = 4;
    public const int NUM_WALLS = 4;

    public Vector2 gridPos;

    public int type; // 0: normal, 1: enter, 2: exit

    // public bool doorTop, doorBot, doorLef, doorRig;

    public bool[] DoorSet = new bool[MAX_DOORS];

    GameObject stairRoom;

    GameObject[] DoorTile;
    SpriteRenderer[] DoorRends;
    BoxCollider2D DoorCollider;

    public GameObject GroundTile = new GameObject();
    SpriteRenderer GroundRend;
    BoxCollider2D[] GroundCollider = new BoxCollider2D[4];

    SpriteRenderer StairDownRend;
    BoxCollider2D StairCollider;

    SpriteRenderer FTUERend;
    private GameObject findroom;

    public void CreateRoom(Vector2 drawPos, Sprite[] Doors, Sprite Ground, Sprite Stair, Sprite FTUESprite)
    {
        GroundRend = GroundTile.AddComponent<SpriteRenderer>();

        GroundTile.name = "Ground";
        //GroundTile.name = "Room " + drawPos.x + " " + (drawPos.y + 0.4f);
        GroundTile.tag = "Ground";
        GroundTile.transform.position = drawPos;
        GroundRend.sprite = Ground;

        DoorTile = new GameObject[MAX_DOORS];
        DoorRends = new SpriteRenderer[MAX_DOORS];

        for (int i = 0; i < MAX_DOORS; i++)
        {
            if (DoorSet[i])
            {
                DoorTile[i] = new GameObject();
                if (i == 0)// Down
                    DoorTile[i].transform.Translate(drawPos.x, drawPos.y + 0.22f, -1);
                if (i == 1)// Up
                    DoorTile[i].transform.Translate(drawPos.x, drawPos.y - 0.22f, -1);
                if (i == 2)// Left
                    DoorTile[i].transform.Translate(drawPos.x + 0.36f, drawPos.y, -1);
                if (i == 3)// Right
                    DoorTile[i].transform.Translate(drawPos.x - 0.34f, drawPos.y, -1);

                DoorTile[i].name = "Door" + (i + 1);
                DoorTile[i].tag = "Door" + (i + 1);
                DoorRends[i] = DoorTile[i].AddComponent<SpriteRenderer>();
                DoorRends[i].sprite = Doors[i];
                DoorTile[i].transform.SetParent(GroundTile.transform);
                DoorCollider = DoorTile[i].AddComponent<BoxCollider2D>();
                GenDoorColliders(i);
                CheckEnd(drawPos, Stair, FTUESprite);

            }
        }

        GenRoomColliders();
    }

    public void MakeStair(Vector2 drawpos, Sprite Stair)
    {
        GroundTile.tag = "Untagged";
        GameObject StairDown = new GameObject();
        StairDown.transform.position = new Vector3(drawpos.x, drawpos.y, -1f);
        StairDown.transform.SetParent(GroundTile.transform);
        StairDown.name = "StairDown";
        StairDown.tag = "StairDown";

        StairDownRend = StairDown.AddComponent<SpriteRenderer>();
        StairDownRend.sprite = Stair;

        StairCollider = StairDown.AddComponent<BoxCollider2D>();
        StairCollider.offset = new Vector2(0f, 0.2f);
        StairCollider.size = new Vector2(0.5f, 0.14f);
        StairCollider.isTrigger = true;

        StairCollider = StairDown.AddComponent<BoxCollider2D>();
        StairCollider.offset = new Vector2(0.3f, 0f);
        StairCollider.size = new Vector2(0.05f, 0.64f);

        StairCollider = StairDown.AddComponent<BoxCollider2D>();
        StairCollider.offset = new Vector2(-0.3f, 0f);
        StairCollider.size = new Vector2(0.05f, 0.64f);

        StairCollider = StairDown.AddComponent<BoxCollider2D>();
        StairCollider.offset = new Vector2(0f, 0.3f);
        StairCollider.size = new Vector2(0.64f, 0.05f);
    }

    public void CheckEnd(Vector2 drawpos, Sprite Stair, Sprite FTUESprite)
    {
        int numDoors = 0;
        int stairRotation = 0;
        for (int i = 0; i < MAX_DOORS; i++)
        {
            if (DoorSet[i])
            {
                numDoors++;
                stairRotation = i + 1;
            }
        }
        if (drawpos.x == 0 && drawpos.y == -0.4f)
        {
            GroundTile.tag = "Untagged";
            findroom = GameObject.FindGameObjectWithTag("Respawn");

            if (findroom == null)
            {
                GameObject FTUE = new GameObject();
                FTUE.transform.position = new Vector3(drawpos.x, drawpos.y, -1f);
                FTUE.transform.SetParent(GroundTile.transform);
                FTUE.name = "FTUE";
                FTUE.tag = "Respawn";

                FTUERend = FTUE.AddComponent<SpriteRenderer>();
                FTUERend.sprite = FTUESprite;
            }
        }
        if (!(drawpos.x == 0 && drawpos.y == -0.4f))
        {
            if (numDoors == 1)
            {
                MakeStair(drawpos, Stair);

                if (stairRotation == 4)
                    StairDownRend.transform.eulerAngles = new Vector3(0, 0, 90);
                if (stairRotation == 2)
                    StairDownRend.transform.eulerAngles = new Vector3(0, 0, 180);
                if (stairRotation == 3)
                    StairDownRend.transform.eulerAngles = new Vector3(0, 0, 270);

            }
        }
    }


    public Room(Vector2 _gridPos, int _type)
    {
        gridPos = _gridPos;
        type = _type;
    }

    public void GenRoomColliders()
    {
        for (int i = 0; i < NUM_WALLS; i++)
        {
            GroundCollider[i] = GroundTile.AddComponent<BoxCollider2D>();
        }
        // BoxCollider for the walls
        GroundCollider[0].offset = new Vector2(0f, 1.73f);
        GroundCollider[0].size = new Vector2(5.61f, 0.43f);

        GroundCollider[1].offset = new Vector2(0f, -1.73f);
        GroundCollider[1].size = new Vector2(5.61f, 0.43f);

        GroundCollider[2].offset = new Vector2(-2.585f, 0f);
        GroundCollider[2].size = new Vector2(0.44f, 3.89f);

        GroundCollider[3].offset = new Vector2(2.59f, 0f);
        GroundCollider[3].size = new Vector2(0.42f, 3.89f);
    }

    public void GenDoorColliders(int i)
    {
        DoorCollider.isTrigger = true;
        if (i == 0)// Down
        {
            DoorCollider.offset = new Vector2(-0.01f, -1.97f);
            DoorCollider.size = new Vector2(0.5f, 0.5f);
        }
        if (i == 1)// Up
        {
            DoorCollider.offset = new Vector2(-0.01f, 1.97f);
            DoorCollider.size = new Vector2(0.5f, 0.5f);
        }
        if (i == 2)// Left
        {
            DoorCollider.offset = new Vector2(-2.96f, 0.01f);
            DoorCollider.size = new Vector2(0.5f, 0.5f);
        }
        if (i == 3)// Right
        {
            DoorCollider.offset = new Vector2(2.95f, 0.01f);
            DoorCollider.size = new Vector2(0.5f, 0.5f);
        }
    }

}
