using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPickup : MonoBehaviour
{
    public Material[] material;
    Renderer rending;
    PlayerCondition condition;

    void Start()
    {
        rending = GetComponent<Renderer>();
        rending.enabled = true;
        rending.sharedMaterial = material[0];
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
    }

    void Update()
    {

    }

    public void OnGazeEnter()
    {
        if (!condition.is_holding)
            rending.sharedMaterial = material[1];
    }

    public void OnGazeExit()
    {
        if (!condition.is_holding)
            rending.sharedMaterial = material[0];
    }

    public void OnClick()
    {
        if (this.tag == "PickUp" && !condition.is_holding) {

            BoxCollider bc = GetComponent<BoxCollider>();
            Rigidbody rb = GetComponent<Rigidbody>();
            GameObject hand = GameObject.Find("Hand");

            condition.is_holding = true;
            bc.enabled = false;
            Destroy(rb);

            this.transform.position = hand.transform.position;
            this.transform.SetParent(hand.transform);

            if (this.name == "Jerrycan")
                condition.get_jerrycan = true;
            else if (this.name.Contains("fire extinguisher")) {

                UIoutput ui = GameObject.Find("Canvas").GetComponent<UIoutput>();

                condition.get_fireEx = true;
                ui.announcing = true;

                if (this.name.EndsWith("B"))
                    ui.announcing_about = 5;
                else if (this.name.EndsWith("C"))
                    ui.announcing_about = 6;
                else
                    ui.announcing_about = 7;
            }
        }
    }
}
