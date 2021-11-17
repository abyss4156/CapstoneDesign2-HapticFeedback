using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToward : MonoBehaviour {

    private Camera mainCam;
    private Transform camTransform;
    private Transform canvasTransform;

	void Start ()
    {
        mainCam = Camera.main;
        camTransform = mainCam.GetComponent<Transform>();
        canvasTransform = GetComponent<Transform>();
    }
	
	void Update () 
    {
        Vector3 screenCenter = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
        canvasTransform.LookAt(screenCenter);
	}
}
