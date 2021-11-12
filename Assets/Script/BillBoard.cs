using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {

    private Transform tr;
    private Transform camTr;

	void Start ()
    {
        tr = GetComponent<Transform>();
        camTr = Camera.main.GetComponent<Transform>();
    }
	
	void Update () 
    {
        tr.LookAt(camTr.position*-1);
	}
}
