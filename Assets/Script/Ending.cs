using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
            SceneManager.LoadScene("Ending");
    }
}
