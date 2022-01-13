using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailOutput : MonoBehaviour
{
    [HideInInspector]
    public bool doorWithFire;
    [HideInInspector]
    public bool putFireEx;
    [HideInInspector]
    public bool waterToElec;
    [HideInInspector]
    public bool getCurtain;

    [HideInInspector]
    public bool notiDetail;

    public Text m_text;
    public RawImage notiImage;
    public AudioSource notiAudio;

    void Start()
    {
        doorWithFire = false;
        putFireEx = false;
        waterToElec = false;
        getCurtain = false;

        notiDetail = false;
    }

    void Update()
    {
        if (notiDetail)
        {
            
        }
    }
}
