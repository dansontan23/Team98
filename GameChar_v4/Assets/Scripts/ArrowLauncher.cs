using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    // Arrow to spawn
    public GameObject arrow;

    // Where to spawn the arrows
    public Transform spawnLocation;

    // Rotation of arrow on spawn
    public Quaternion spawnRotation;

    // Supposedly to create time delay that the projectile spawns after, 
    // but doesn't work, not sure how to fix
    public float spawnTime = 3.0f;

    // Time since the arrow spawned at
    private float timeSinceSpawned = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        timeSinceSpawned += Time.deltaTime;

        if(Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E)) {
            Instantiate(arrow, spawnLocation.position, spawnRotation);
            timeSinceSpawned = 0;
        }
    }
}

