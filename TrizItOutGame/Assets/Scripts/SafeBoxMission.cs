using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SafeBoxMission : MonoBehaviour
{
    [SerializeField]
    private string m_Password = "1234";

    [SerializeField]
    private Sprite m_SafeBoxOpenZoomSprite;

    [SerializeField]
    private GameObject m_ScrewDriver;

    private int m_IndexOfDigitOnScreen = 0;

    [SerializeField]
    public TextMeshProUGUI uiText;

    private DisplayManagerLevel1 m_DisplayManager;
    private string m_CurrentPassCode = null;

    void Start()
    {
        m_DisplayManager = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();

        if(m_DisplayManager ==  null)
        {
            Debug.LogError("DisplayManager in SafeBoxMission is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CodeFunction(string Number)
    {
        if (m_IndexOfDigitOnScreen < 4)
        {
            m_IndexOfDigitOnScreen++;
            m_CurrentPassCode = m_CurrentPassCode + Number;
            uiText.text = m_CurrentPassCode;
        }
    }

    public void CheckPassCode()
    {
        if(m_CurrentPassCode == m_Password)
        {
            uiText.text = "Correct!!";
            StartCoroutine(waiter(5));
            m_DisplayManager.GetComponent<SpriteRenderer>().sprite = m_SafeBoxOpenZoomSprite;
            m_ScrewDriver.SetActive(true);
        }
        else
        {
            Debug.Log("try again!");
            ResetPassCode();
        }
    }

    IEnumerator waiter(int sec)
    {
        yield return new WaitForSeconds(sec);
    }

    public void ResetPassCode()
    {
        m_IndexOfDigitOnScreen = 0;
        m_CurrentPassCode = null;
        uiText.text = m_CurrentPassCode;
    }

}
