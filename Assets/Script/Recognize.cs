using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recognize : MonoBehaviour
{
    PlayerCondition condition;
    MsgListener msgListener;

    void Start()
    {
        condition = GetComponent<PlayerCondition>();
        msgListener = GameObject.Find("SerialController").GetComponent<MsgListener>();
    }
    
    void Update()
    {
        if (condition.get_curtain && condition.is_curtainWatered)
            msgListener.change_message(5);
    }
}
