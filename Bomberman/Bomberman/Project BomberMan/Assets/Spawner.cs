using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectToSpawn;
    private Transform spawner;

    // Start is called before the first frame update
    void Awake()
    {
        spawner = this.gameObject.transform;
        //GameObject ground = GameObject.Find("Enemies");


        // Spawns a Random Item/Enemy From the list
        GameObject spawned = Instantiate(objectToSpawn[Random.Range(0, objectToSpawn.Length)], this.gameObject.transform.position, Quaternion.identity);
        spawned.transform.parent = spawner.transform;
        
        
       //Destroy(this.gameObject);
    }
}
