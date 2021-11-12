using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCondition : MonoBehaviour
{
    UIoutput ui;
    DoorRotation doorRotation;
    AudioSource openSound;
    bool playOnce;
    PlayerCondition condition;

    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIoutput>();
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
        doorRotation = GetComponent<DoorRotation>();
        openSound = GetComponent<AudioSource>();
        playOnce = false;
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player") {

            if (playOnce)
                return;

            if (condition.get_towel && condition.is_towelWatered) {

                openSound.Play();
                playOnce = true;
                doorRotation.isOpen = true;
            }
            else {
                ui.warning = true;
                ui.warning_about = 1;
            }
        }
    }
}
