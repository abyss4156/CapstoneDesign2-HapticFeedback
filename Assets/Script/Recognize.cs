using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recognize : MonoBehaviour
{
    public GameObject hand;
    public ParticleSystem water;
    public AudioSource pickupsound;

    private Vector3 ScreenCenter;
    RaycastHit hit;
    ObjectGlow objGlow;

    PlayerCondition condition;
    UIoutput ui;
    AudioSource waterSound;

    void Start()
    {
        hand = GameObject.Find("Hand");
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight * 4 / 5);
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
        ui = GameObject.Find("Canvas").GetComponent<UIoutput>();

        waterSound = water.GetComponent<AudioSource>();
        var emission = water.emission;
        emission.enabled = false;
    }

    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);

        if (Physics.Raycast(ray, out hit, 50.0f)) {

            string tag = hit.collider.gameObject.tag;

            if (tag == "PickUp" || tag == "Item") {

                objGlow = hit.collider.GetComponent<ObjectGlow>();
                objGlow.Selected();
            }
            else if (objGlow != null)
                objGlow.NotSelected();

            // OVRInput.Get(OVRInput.Button.One)
            // Input.GetButtonDown("Fire1")
            if (tag == "PickUp" && !condition.is_holding && Input.GetButtonDown("Fire1")) {

                BoxCollider boxcol = hit.collider.GetComponent<BoxCollider>();
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

                condition.is_holding = true;
                boxcol.enabled = false;
                Destroy(rb);

                hit.transform.position = hand.transform.position;
                hit.transform.rotation.Set(-90,hand.transform.rotation.y ,hit.transform.rotation.z, hit.transform.rotation.w);
                hit.transform.SetParent(hand.transform);

                if (hit.collider.gameObject.name == "Jerrycan")
                    condition.get_jerrycan = true;
                else if (hit.collider.gameObject.name.Contains("fire extinguisher"))
                    condition.get_fireEx = true;
            }

            // OVRInput.Get(OVRInput.Button.One)
            // Input.GetButtonDown("Fire1")
            if (tag == "Item" && Input.GetButtonDown("Fire1")) {

                string name = hit.collider.gameObject.name;

                if (name == "towel")
                    condition.get_towel = true;
                else if (name == "curtain1" || name == "curtain2")
                    condition.get_curtain = true;

                pickupsound.Play();
                hit.collider.gameObject.SetActive(false);
            }

            // OVRInput.Get(OVRInput.Button.One)
            // Input.GetButtonDown("Fire1")
            if (hit.collider.gameObject.name == "sink" && Input.GetButtonDown("Fire1")) {

                var emission = water.emission;

                if (!emission.enabled) {

                    waterSound.Play();
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
            }
        }
    }
}
