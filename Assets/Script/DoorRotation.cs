using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour 
{
    public float rotationV = 1.0f;
    float currentDeg = 0f;
    public float rotationDeg = 90.0f;
    public bool isOpen = false;
	
	void Update () 
    {
        if (isOpen)
            OpenDoor();    
	}

    public void OpenDoor()
    {
        if (currentDeg <= rotationDeg) {

            currentDeg += rotationV;
            transform.Rotate(0, rotationV, 0);
        }
        else
            return;
    }
}
