﻿///////////////////////////////////////////////////////////////////////////////////////////
// Creates rooms in a grid with a max number of rooms checking to see how many neighbors
// are next to them.
// Also sets the amount of doors the room has using the number of neighbors.
// Places all needed rooms(clones of levelgenerator)
// **Attach 4 doors** **Attach Ground** **Attach Stairs** **Attach FTUESprite**
///////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    Vector2 worldSize = new Vector2(3, 3);
    Room[,] rooms;
    List<Vector2> takenPositions = new List<Vector2>();
    int gridSizeX, gridSizeY, numberOfRooms = 20;
    //   public GameObject mapSprite;
    [Header("0: Down\t1: Up\t2: Left\t3: Right")]
    [SerializeField]
    private Sprite[] Doors = new Sprite[Room.MAX_DOORS];
    // Element 0 = Down
    // Element 1 = Up
    // Element 2 = Left
    // Element 3 = Right
    public Sprite Ground;

    public PlayerCollision pc;

    public Room StaticRoom;

    private GameObject GroundRemaining;

    public Sprite Stair;
    public Sprite FTUESprite;

    public bool reset;
    public bool done = false;
    void Start()
    {

        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        { // make sure we dont try to make more rooms than can fit in our grid
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x); //note: these are half-extents
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms(); //lays out the actual map
        SetRoomDoors(); //assigns the doors where rooms would connect
        DrawMap(); //instantiates objects to make up a map
    }
    void CreateRooms()
    {
        //setup
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;
       
        //magic numbers
        //makes the rooms spread out rather than be clustered
        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;
        //add rooms
        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            float randomPerc = ((float)i) / (((float)numberOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            //grab new position
            checkPos = NewPosition();
            //test new position
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50)
                    print("error: could not create with fewer neighbors than : " + NumberOfNeighbors(checkPos, takenPositions));
            }
            //finalize position
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0);
            takenPositions.Insert(0, checkPos);
        }
    }
    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1)); // pick a random room
            x = (int)takenPositions[index].x;//capture its x, y position
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);//randomly pick wether to look on hor or vert axis
            bool positive = (Random.value < 0.5f);//pick whether to be positive or negative on that axis
            if (UpDown)
            { //find the position bnased on the above bools
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); //make sure the position is valid
        return checkingPos;
    }
    Vector2 SelectiveNewPosition()
    { // method differs from the above in the two commented ways
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                //instead of getting a room to find an adject empty space, we start with one that only 
                //as one neighbor. This will make it more likely that it returns a room that branches out
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if (inc >= 100)
        { // break loop if it takes too long: this loop isnt garuanteed to find solution, which is fine for this
            print("Error: could not find position with only one neighbor");
        }
        return checkingPos;
    }
    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0; // start at zero, add 1 for each side there is already a room
        if (usedPositions.Contains(checkingPos + Vector2.right))
        { //using Vector.[direction] as short hands, for simplicity
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        return ret;
    }
    void DrawMap()
    {
        foreach (Room room in rooms)
        {
            if (room == null)
            {
                continue; //skip where there is no room
            }
            Vector2 drawPos = room.gridPos;
            drawPos.x *= 6f;//aspect ratio of map sprite
            drawPos.y *= 4f;
            drawPos.y -= 0.4f;
            room.CreateRoom(drawPos, Doors, Ground, Stair, FTUESprite);
 
        }
       
    }
    void SetRoomDoors()
    {
        for (int x = 0; x < ((gridSizeX * 2)); x++)
        {
            for (int y = 0; y < ((gridSizeY * 2)); y++)
            {
                if (rooms[x, y] == null)
                {
                    continue;
                }
                Vector2 gridPosition = new Vector2(x, y);

                if (y - 1 < 0)
                { //check above
                    rooms[x, y].DoorSet[0] = false;
                }
                else
                {
                    rooms[x, y].DoorSet[0] = (rooms[x, y - 1] != null);
                }
                if (y + 1 >= gridSizeY * 2)
                { //check bellow
                    rooms[x, y].DoorSet[1] = false;
                }
                else
                {
                    rooms[x, y].DoorSet[1] = (rooms[x, y + 1] != null);
                }
                if (x - 1 < 0)
                { //check left
                    rooms[x, y].DoorSet[2] = false;
                }
                else
                {
                    rooms[x, y].DoorSet[2] = (rooms[x - 1, y] != null);
                }
                if (x + 1 >= gridSizeX * 2)
                { //check right
                    rooms[x, y].DoorSet[3] = false;
                }
                else
                {
                    rooms[x, y].DoorSet[3] = (rooms[x + 1, y] != null);
                }

            }
        }
    }



    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {

            reset = true;

        }
        reset = pc.reset;
        if (reset)
        {
            if (GroundRemaining = GameObject.Find("Ground"))
            {
                Destroy(GroundRemaining);
            }
            else
            {
                reset = false;
                done = true;
                resetFloor();

                if (!GameObject.Find("StairDown"))
                {
                    foreach (Room room in rooms)
                    {
                        if (room != null)
                        {
                            room.MakeStair(room.GroundTile.transform.position, Stair);
                            break;
                        }
                    }

                }
            }

        }
    }

    public void resetFloor()
    {

        takenPositions = new List<Vector2>();

        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        { // make sure we dont try to make more rooms than can fit in our grid
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x); //note: these are half-extents
        gridSizeY = Mathf.RoundToInt(worldSize.y);

        CreateRooms(); //lays out the actual map
        SetRoomDoors(); //assigns the doors where rooms would connect
        DrawMap(); //instantiates objects to make up a map

    }
}
