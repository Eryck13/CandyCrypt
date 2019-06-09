using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour
{

    public int tileX;
    public int tileY;
    public TileMap map;
    private GameObject[] player;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    private void Update()
    {
        tileX = (int)player[0].transform.position.x;
        tileY = (int)player[0].transform.position.y;
        map.GeneratePathTo(tileX, tileY);
    }

    //void OnMouseUp()
    //{
    //    //	Debug.Log ("Click!");
    //    Vector2 pPos = player.transform.localPosition;

    //    map.GeneratePathTo((int)pPos.x, (int)pPos.y);
    //}

}
