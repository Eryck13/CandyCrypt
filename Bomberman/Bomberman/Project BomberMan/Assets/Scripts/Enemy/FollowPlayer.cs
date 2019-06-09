using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;


    public float tileX;
    public float tileY;
    public TileMap map;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tileX = player.transform.position.x;
        tileY = player.transform.position.y;
        map.GeneratePathTo((int)tileX, (int)tileY);
    }
}
