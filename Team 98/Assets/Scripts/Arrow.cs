using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    
    // public float force;

    // Constant speed of arrow
    public float moveSpeed = 16.0f;

    // Time that arrow persists for before it is destroyed
    public float timeToLive = 2.0f;

    // Time since the arrow spawned at; to control when it is destroyed
    private float timeSinceSpawned = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        // rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - 180);
    }

    // Update is called once per frame
    void Update()
    {        
        transform.position += moveSpeed * transform.right * Time.deltaTime;

        // timeSinceSpawned increases as time passes
        timeSinceSpawned += Time.deltaTime;

        // Destroy the arrow when it has travelled for 2 seconds
        if(timeSinceSpawned > timeToLive) {
            Destroy(gameObject);
        }
    }

    // What happens when arrow collides with an object/enemy: (need to add damage/impact logic)
    void OnTriggerEnter2D(Collider2D collider) 
    {
        Destroy(gameObject);
    }
}
