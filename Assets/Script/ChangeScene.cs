using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private Vector3 ScreenCenter;
    private RaycastHit hit;

    void Start()
    {
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight * 4 / 5);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);

        if (Physics.Raycast(ray, out hit, 100.0f)) {

            if (hit.collider.gameObject.name == "TitleButton") {

                // if (Input.GetButtonDown("Fire1")) {
                if (OVRInput.Get(OVRInput.Button.One)) {
                    SceneManager.LoadScene("SampleScene");
                }
            }
            else if (hit.collider.gameObject.name == "EndingButton") {

                // if (Input.GetButtonDown("Fire1")) {
                if (OVRInput.Get(OVRInput.Button.One)) {
                    SceneManager.LoadScene("Title");
                }
            }
        }
    }
}
