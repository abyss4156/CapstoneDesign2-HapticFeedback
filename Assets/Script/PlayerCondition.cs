using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    [HideInInspector]
    public bool is_holding;
    [HideInInspector]
    public bool get_fireEx;
    [HideInInspector]
    public bool get_jerrycan;
    [HideInInspector]
    public bool is_jerrycanWatered;
    [HideInInspector]
    public bool get_towel;
    [HideInInspector]
    public bool is_towelWatered;
    [HideInInspector]
    public bool is_electricTurnOff;
    [HideInInspector]
    public bool get_curtain;
    [HideInInspector]
    public bool is_curtainWatered;

    MsgListener msgListener;

    [HideInInspector]
    public bool cooling;
    private float coolingTime;

    bool start;
    private float startTime;

    void Start()
    {
        is_holding = false;
        get_fireEx = false;
        get_jerrycan = false;
        is_jerrycanWatered = false;
        get_towel = false;
        is_towelWatered = false;
        is_electricTurnOff = false;
        get_curtain = false;
        is_curtainWatered = false;

        cooling = false;
        coolingTime = 5.0f;

        start = true;
        startTime = 5.0f;

        msgListener = GameObject.Find("SerialController").GetComponent<MsgListener>();
    }

    void Update()
    {
        if (get_curtain && is_curtainWatered)
            msgListener.change_message(5);

        if (cooling) {

            coolingTime -= Time.deltaTime;

            if (coolingTime > 0)
                msgListener.change_message(5);
            else {

                coolingTime = 5.0f;
                msgListener.change_message(-5);
                cooling = false;
            }
        }

        if (start) {

            startTime -= Time.deltaTime;

            if (startTime > 0) {

                msgListener.change_message(1);
                msgListener.change_message(4);
            }
            else {

                msgListener.change_message(-1);
                msgListener.change_message(-4);
                start = false;
            }
        }
    }
}
