using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickActivate : MonoBehaviour
{
    public ParticleSystem waterStream;
    public ParticleSystem lightning;
    public ParticleSystem waterDrop;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        if (this.tag == "Activate") {

            PlayerCondition condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
            UIoutput ui = GameObject.Find("Canvas").GetComponent<UIoutput>();

            if (this.name == "sink") {

                MsgListener msgListener = GameObject.Find("SerialController").GetComponent<MsgListener>();

                var emission = waterStream.emission;

                if (!emission.enabled) {

                    AudioSource sfx_waterStream = waterStream.GetComponent<AudioSource>();

                    sfx_waterStream.Play();
                    emission.enabled = true;
                }

                if (condition.get_jerrycan && !condition.is_jerrycanWatered) {

                    condition.is_jerrycanWatered = true;
                    ui.announcing_about = 2;
                    ui.announcing = true;
                }
                else if (condition.get_towel && !condition.is_towelWatered) {

                    condition.is_towelWatered = true;
                    ui.announcing_about = 1;
                    ui.announcing = true;
                }
                else if (condition.get_curtain && !condition.is_curtainWatered) {

                    condition.is_curtainWatered = true;
                    ui.announcing_about = 3;
                    ui.announcing = true;
                }

                if (!condition.is_curtainWatered)
                    condition.cooling();
            }
            else if (this.name == "ElectroPanel") {

                if (condition.is_electricTurnOff) {

                    AudioSource sfx_lightning = lightning.GetComponent<AudioSource>();
                    BoxCollider bc = waterDrop.GetComponent<BoxCollider>();
                    var emission = lightning.emission;

                    condition.is_electricTurnOff = true;
                    emission.enabled = false;
                    sfx_lightning.Stop();
                    bc.isTrigger = true;

                    ui.announcing_about = 4;
                    ui.announcing = true;
                }
            }
        }
    }
}