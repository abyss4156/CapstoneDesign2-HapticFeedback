using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningFire : MonoBehaviour
{
    public ParticleSystem smoke;

    private bool is_start;
    private PlayerCondition condition;

    private float currentTime;
    private float endTime = 1.5f;

    private void Start()
    {
        is_start = false;
        condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
    }

    private void Update()
    {
        if (is_start)
        {
            currentTime += Time.deltaTime;
            Fire_off();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player") {

            if (condition.get_fireEx && smoke.emission.enabled)
                is_start = true;
        }
    }

    void Fire_off()
    {
        if (transform.localScale != Vector3.zero)
        {
            if (currentTime < endTime)
            {
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                //Debug.Log(transform.localScale);
            }
            else if(currentTime >= endTime){
                Destroy(this.gameObject);
                return;
            }
        }
    }
}
