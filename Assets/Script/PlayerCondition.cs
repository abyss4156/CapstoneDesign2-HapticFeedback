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

    private float coolingTime;

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

        msgListener = GameObject.Find("SerialController").GetComponent<MsgListener>();
    }

    void Update()
    {
        if (get_curtain && is_curtainWatered)
            msgListener.change_message(5);
    }

    public void cooling()
    {
        coolingTime = 5.0f;

        while (coolingTime > 0) {

            coolingTime -= Time.deltaTime;
            msgListener.change_message(5);
        }

        msgListener.change_message(-5);
    }
}
