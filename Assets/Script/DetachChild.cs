using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachChild : MonoBehaviour {
    
    private Rigidbody rb;
    private BoxCollider bc;
    private PlayerCondition condition;

	void Start () {
        
        //rb = gameObject.GetComponent<Rigidbody>();
        bc = gameObject.GetComponent<BoxCollider>();
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
	}
	
	void Update ()
    {
        // OVRInput.Get(OVRInput.Button.Two)
        if (Input.GetButtonDown("Fire2") && condition.is_holding && this.transform.parent != null) {

            this.transform.parent.DetachChildren();
            rb = this.gameObject.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            bc.enabled = true;

            condition.is_holding = false;
            condition.get_jerrycan = false;
            condition.get_fireEx = false;
        }
	}
}
