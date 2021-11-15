using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour {

    public ParticleSystem powder;
    public ParticleSystem smallFire;
    public AudioSource fbxShooting;
    
    PlayerCondition condition;

    private float lastTime = 1.5f;

    void Start()
    {
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();

        var emission = powder.emission;
        emission.enabled = false;
    }

	void Update () 
    {
        if (condition.get_fireEx && this.transform.parent.parent) {

            var emission = powder.emission;

            //if (Input.GetButtonDown("Fire1")) {
            if (OVRInput.Get(OVRInput.Button.One)) {

                if (!emission.enabled) {

                    emission.enabled = true;
                    fbxShooting.Play();
                }
                else {

                    fbxShooting.Stop();
                    emission.enabled = false;
                }
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.name == smallFire.name) {

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
    }
}
