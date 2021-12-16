using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItem : MonoBehaviour
{
    public Material[] material;

    Renderer rending;

    void Start()
    {
        rending = GetComponent<Renderer>();
        rending.enabled = true;
        rending.sharedMaterial = material[0];
    }

    void Update()
    {

    }

    public void OnGazeEnter()
    {
        rending.sharedMaterial = material[1];
    }

    public void OnGazeExit()
    {
        rending.sharedMaterial = material[0];
    }

    public void OnClick()
    {
        if (this.tag == "Item") {

            PlayerCondition condition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCondition>();
            AudioSource sfx_pickup = GameObject.Find("PickUp").GetComponent<AudioSource>();

            if (this.name == "towel")
                condition.get_towel = true;
            else if (this.name.Contains("curtain"))
                condition.get_curtain = true;

            sfx_pickup.Play();
            this.gameObject.SetActive(false);
        }
    }
}
