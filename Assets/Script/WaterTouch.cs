using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTouch : MonoBehaviour
{
    PlayerCondition condition;
    MsgListener msgListener;
    UIoutput ui;

    void Start()
    {
        msgListener = GameObject.Find("SerialController").GetComponent<MsgListener>();
        ui = GameObject.Find("Canvas").GetComponent<UIoutput>();
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player") {

            ui.warning_about = 6;
            ui.warning = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") {

            msgListener.send_message(3);
            msgListener.send_message(5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {

            msgListener.send_message(-3);
            msgListener.send_message(-5);
        }
    }
}
