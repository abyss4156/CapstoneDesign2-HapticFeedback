using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGlow : MonoBehaviour
{
    public Material[] materials;
    Renderer rend;
    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
    }
	// Update is called once per frame
	void Update()
    {
    }

    public void Selected()
    {
        rend.sharedMaterial = materials[1];
    }
    public void NotSelected(){
        rend.sharedMaterial = materials[0];
    }
}
