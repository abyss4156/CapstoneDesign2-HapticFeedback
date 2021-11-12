using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour {

    public ParticleSystem powder;

    AudioSource fireExSound;
    PlayerCondition condition;

    void Start()
    {
        fireExSound = GetComponent<AudioSource>();
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();

        var emission = powder.emission;
        emission.enabled = false;
    }

	void Update () 
    {
        if (condition.get_fireEx) {

            var emission = powder.emission;

            // OVRInput.Get(OVRInput.Button.One)
            if (Input.GetButtonDown("Fire1")) {

                if (!emission.enabled) {

                    emission.enabled = true;
                    fireExSound.Play();
                }
                else {

                    fireExSound.Stop();
                    emission.enabled = false;
                }
            }
        }
	}
}
