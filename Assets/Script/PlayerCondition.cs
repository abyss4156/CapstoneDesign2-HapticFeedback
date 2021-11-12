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
    public bool get_hammer;
    [HideInInspector]
    public bool get_curtain;
    [HideInInspector]
    public bool is_curtainWatered;

    void Start()
    {
        is_holding = false;
        get_fireEx = false;
        get_jerrycan = false;
        is_jerrycanWatered = false;
        get_towel = false;
        is_towelWatered = false;
        get_hammer = false;
        get_curtain = false;
        is_curtainWatered = false;
    }

    void Update()
    {
        
    }
}
