using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreation : MonoBehaviour
{
    //Set Room Layouts (Segmented)
    [SerializeField]
    private GameObject[] TL; // Top Left
    [SerializeField]
    private GameObject[] TR; // Top Right
    [SerializeField]
    private GameObject[] BL; // Bottom Left
    [SerializeField]
    private GameObject[] BR; // Bottom Right
    [SerializeField]
    private GameObject[] C; // Center
    [SerializeField]
    private GameObject[]  Enemies;
    [SerializeField]
    private GameObject  Items;

    private GameObject findroom;

    // Update is called once per frame
    void Update()
    {
        findroom = GameObject.FindGameObjectWithTag("Ground");

        if (findroom != null)
        {
            FillRoomLocation(TL, findroom);
            FillRoomLocation(TR, findroom);
            FillRoomLocation(BL, findroom);
            FillRoomLocation(BR, findroom);
            FillRoomLocation(C, findroom);
            GiveRoomItem(Items, findroom);
            SpawnEnemies(Enemies, findroom);
            findroom.tag = "Untagged";
            findroom = null;
        }
    }


    void FillRoomLocation(GameObject[] LT, GameObject room)
    {
       
        int random = Random.Range(0, LT.Length);
        Vector3 position = room.transform.position - new Vector3(0, 0, 3);
        Quaternion rotation = room.transform.rotation;

        // Setting a random preset room at set location
        GameObject test = Instantiate<GameObject>(LT[random], position, rotation);
        test.transform.parent = room.transform;
    }

    void GiveRoomItem(GameObject item, GameObject room)
    {
        int random = Random.Range(0 , 100);
        Vector3 position = room.transform.position - new Vector3(0, 0, 3);
        Quaternion rotation = room.transform.rotation;

        if (random < 40)
        {
            GameObject itemspawn = Instantiate<GameObject>(item, position, rotation);
            itemspawn.transform.parent = room.transform;
        }


    }

    void SpawnEnemies(GameObject[] Enemies, GameObject room)
    {
        int random = Random.Range(0, 100);
        Vector3 position = room.transform.position - new Vector3(0, 0, 3);
        Quaternion rotation = room.transform.rotation;

        //Number of enemies Spawned
        if (random > 20)
        {
            GameObject itemspawn = Instantiate<GameObject>(Enemies[0], position, rotation);
            itemspawn.transform.parent = room.transform;
        }

        if (random > 40)
        {
            GameObject itemspawn2 = Instantiate<GameObject>(Enemies[1], position, rotation);
            itemspawn2.transform.parent = room.transform;
        }

        if (random > 60)
        {
            GameObject itemspawn3 = Instantiate<GameObject>(Enemies[2], position, rotation);
            itemspawn3.transform.parent = room.transform;
        }

        if (random > 80)
        {
            GameObject itemspawn4 = Instantiate<GameObject>(Enemies[3], position, rotation);
            itemspawn4.transform.parent = room.transform;
        }


    }


}
