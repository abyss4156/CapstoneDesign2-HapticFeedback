using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeTouch : MonoBehaviour
{
    PlayerCondition condition;
    MsgListener msgListener;

    void Start()
    {
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
        msgListener = GameObject.Find("SerialController").GetComponent<MsgListener>();
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") {

            if (!condition.is_curtainWatered) {

                msgListener.send_message(4);
                msgListener.send_message(6);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {

            msgListener.send_message(-4);
            msgListener.send_message(-6);
        }
    }
}
