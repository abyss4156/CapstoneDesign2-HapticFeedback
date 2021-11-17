using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour {

    public ParticleSystem powder;
    public ParticleSystem smallFire;
    public AudioSource fbxShooting;
    
    PlayerCondition condition;
    MsgListener msgListener;
    UIoutput ui;

    private float lastTime = 1.5f;

    void Start()
    {
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
        msgListener = GameObject.Find("SerialController").GetComponent<MsgListener>();
        ui = GameObject.Find("Canvas").GetComponent<UIoutput>();

        var emission = powder.emission;
        emission.enabled = false;
    }

	void Update () 
    {
        if (condition.get_fireEx && this.transform.parent.parent) {

            var emission = powder.emission;

            if (Input.GetButtonDown("Fire1")) {
            //if (OVRInput.Get(OVRInput.Button.One)) {

                if (!emission.enabled) {

                    emission.enabled = true;
                    fbxShooting.Play();
                    msgListener.change_message(2);
                }
                else {

                    fbxShooting.Stop();
                    emission.enabled = false;
                    msgListener.change_message(-2);
                }
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Fireball_big_red_Door") {

            if (this.transform.parent.name.Contains("C")) {

                if (lastTime > 0) {

                    lastTime -= Time.deltaTime;
                    smallFire.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                }
                else {

                    Destroy(smallFire.gameObject);
                    GetComponent<BoxCollider>().enabled = false;
                    return;
                }
            }
            else {

                ui.warning_about = 7;
                ui.warning = true;
            }
        }
    }
}
