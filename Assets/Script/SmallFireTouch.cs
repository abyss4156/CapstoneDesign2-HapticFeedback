using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallFireTouch : MonoBehaviour {

    UIoutput ui;
    PlayerCondition condition;

	void Start () 
    {
        ui = GameObject.Find("Canvas").GetComponent<UIoutput>();
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
	}
	
	void Update () 
    {

	}

    void OnCollisionEnter (Collision collision)
    {
        if (collision.collider.gameObject.name == "Player") {

            ui.warning = true;

            if (condition.get_jerrycan && condition.is_jerrycanWatered)
                ui.warning_about = 4;
            else
                ui.warning_about = 2;
        }
    }
}
