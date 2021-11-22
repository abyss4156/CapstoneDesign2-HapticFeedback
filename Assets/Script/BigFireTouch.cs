using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigFireTouch : MonoBehaviour {

    UIoutput ui;
    PlayerCondition condition;

    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIoutput>();
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player") {

            if (condition.get_curtain && condition.is_curtainWatered) {

                BoxCollider boxCollider = GetComponent<BoxCollider>();
                boxCollider.enabled = false;
            }
            else {

                ui.warning = true;

                if (!condition.get_curtain)
                    ui.warning_about = 3;
                else
                    ui.warning_about = 5;
            }
        }
    }
}
