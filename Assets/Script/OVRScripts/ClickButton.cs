using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour
{
    Camera cam;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        cam = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
    }

    void Update()
    {
        ray = new Ray(cam.transform.position, cam.transform.rotation * Vector3.forward);
    }

    public void OnGazeEnter()
    {
        if (Physics.Raycast(ray, out hit, 2.0f))
            this.GetComponent<Image>().color = new Color32(100, 100, 100, 100);
    }

    public void OnGazeExit()
    {
        this.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
    }

    public void OnClick()
    {
        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            if (this.name == "TitleButton")
                SceneManager.LoadScene("SampleScene");
            else if (this.name == "EndingButton")
                SceneManager.LoadScene("Title");
        }
    }
}
