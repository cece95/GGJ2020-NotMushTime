using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController Attached_Door;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Attached_Door);
    }

    void OnTriggerEnter2D()
    {
        // Depress button


        // Open Door
        Attached_Door.OpenDoor();
    }

    void OnTriggerExit2D()
    {
        // Raise button


        // Close Door
        Attached_Door.CloseDoor();

    }
}