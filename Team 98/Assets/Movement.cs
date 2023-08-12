using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float LRAmount = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(LRAmount,0,0);
        float UDAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0,UDAmount,0);
    }
}
