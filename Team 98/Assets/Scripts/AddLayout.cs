using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLayout : MonoBehaviour
{
    private RoomLayouts roomLayouts;

    void Start()
    {
        Debug.Log("wat is happening?");
        roomLayouts = GameObject.FindGameObjectWithTag("Layouts").GetComponent<RoomLayouts>();
        roomLayouts.layouts.Add(this.gameObject);
    }
}
