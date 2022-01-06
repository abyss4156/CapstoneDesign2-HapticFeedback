using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGazeEnter()
    {
        this.GetComponent<Image>().color = new Color32(100, 100, 100, 100);
    }

    public void OnGazeExit()
    {
        this.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
    }

    public void OnClick()
    {
        if (this.name == "TitleButton")
            SceneManager.LoadScene("SampleScene");
        else if (this.name == "EndingButton")
            SceneManager.LoadScene("Title");
    }
}
