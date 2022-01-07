using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickItem : MonoBehaviour
{
    public Material[] material;

    Renderer rending;

    Camera cam;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        rending = GetComponent<Renderer>();
        rending.enabled = true;
        rending.sharedMaterial = material[0];

        cam = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
    }

    void Update()
    {
        ray = new Ray(cam.transform.position, cam.transform.rotation * Vector3.forward);
    }

    public void OnGazeEnter()
    {
        if (Physics.Raycast(ray, out hit, 2.0f))
            rending.sharedMaterial = material[1];
    }

    public void OnGazeExit()
    {
        rending.sharedMaterial = material[0];
    }

    public void OnClick()
    {
        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            if (this.tag == "Item")
            {
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
}
