using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayouts : MonoBehaviour
{
    public GameObject startingLayout;
    public List<GameObject> layouts;
    public GameObject[] allLayouts;


    void Start()
    {
        Instantiate(startingLayout, transform.position, Quaternion.identity);
    }
}
