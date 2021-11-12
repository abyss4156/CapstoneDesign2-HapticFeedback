using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIoutput : MonoBehaviour {

    [HideInInspector]
    public bool warning;
    [HideInInspector]
    public int warning_about;
    [HideInInspector]
    public bool announcing;
    [HideInInspector]
    public int announcing_about;
    public GameObject UIpanel;
    public Text m_text;

    float timeLimit;
    float currentTime;

    void Start () 
    {
        warning = false;
        warning_about = 0;
        announcing = false;
        announcing_about = 0;
        UIpanel.SetActive(false);
        m_text.text = "";

        timeLimit = 3.0f;
        currentTime = 0.0f;
	}
	
	void Update () 
    {
        if (warning) {

            switch (warning_about) {
                // 문
                case 1:
                    m_text.text = "문 밖에서 연기가 들어온다.\n이 상태로 나갈 수 없을 것 같다.";
                    break;
                // 작은 불
                case 2: 
                    m_text.text = "들어가려면 불을 꺼야 될 것 같다.";
                    break;
                // 큰 불
                case 3: 
                    m_text.text = "이 불은 끄지 못할 것 같다.";
                    break;
                // 작은 불, jerrycan 잡은 상태에서
                case 4: 
                    m_text.text = "전기 화재에 물을 뿌리는 건\n위험해 보인다.";
                    break;
                // 큰 불, towel이 젖지 않은 상태에서
                case 5: 
                    m_text.text = "불을 통과하기 위해선\n커튼을 적시고 몸을 감싸야 겠다.";
                    break;
                default:
                    break;
            }

            UIpanel.SetActive(true);

            currentTime += Time.deltaTime;
            if (currentTime >= timeLimit) {
                warning = false;
                currentTime = 0.0f;
            }
        }
        else if (announcing) {

            switch (announcing_about) {
                case 1: 
                    m_text.text = "손수건을 적셨다!";
                    break;
                case 2: 
                    m_text.text = "통에 물을 채워 넣었다!";
                    break;
                case 3: 
                    m_text.text = "커튼을 적셨다! 이제 나갈 수 있겠다!";
                    break;
                default:
                    break;
            }

            UIpanel.SetActive(true);

            currentTime += Time.deltaTime;
            if (currentTime >= timeLimit){
                announcing = false;
                currentTime = 0.0f;
            }
        }
        else {
            UIpanel.SetActive(false);
            m_text.text = "";
        }

        Debug.Log(m_text.text);
    }
}
