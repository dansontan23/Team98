using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{

    public GameObject[] startingRooms;

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public int maxRooms = 10;
    public int roomCount = 0;

    [SerializeField] float waitTime = 10f;
    private bool spawnedBoss = false;
    public GameObject boss;

    public RoomLayouts roomLayouts;

    void Start()
    {
        StartRoomGeneration();
    }

    void StartRoomGeneration()
    {
        int rand = Random.Range(0, startingRooms.Length);
        roomLayouts = GameObject.FindGameObjectWithTag("Layouts").GetComponent<RoomLayouts>();
        Instantiate(startingRooms[rand], transform.position, Quaternion.identity);
    }

    void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false)
        {
            Destroy(roomLayouts.layouts[roomCount-2]);
            Instantiate(boss, rooms[roomCount-1].transform.position, Quaternion.identity);
            spawnedBoss = true;
            DeleteSpawns(); 
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    public void Counter()
    {
        roomCount++;
        if (roomCount >= maxRooms)
        {
            return;
        }
    }

    void DeleteSpawns()
    {
        RoomSpawner[] spawns = FindObjectsOfType<RoomSpawner>();
        foreach(RoomSpawner spawn in spawns)
        {
            Destroy(spawn.gameObject);
        }
    }
}
