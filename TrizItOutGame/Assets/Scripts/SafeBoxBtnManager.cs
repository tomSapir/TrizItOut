using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeBoxBtnManager : MonoBehaviour
{
    [SerializeField]
    private Button[] m_NumButtons;

    [SerializeField]
    private TextMeshProUGUI m_screenText;

    [SerializeField]
    private GameObject m_SafeBoxMission;

    string PassCode = "1234";
    string CurrentPassCode = null;
    private int m_Index = 0;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnClickSafeBoxNumberBtn(string Number)
    {
        if (m_Index < 4)
        {
            m_Index++;
            CurrentPassCode = CurrentPassCode + Number;
            m_screenText.text = CurrentPassCode;
        }
    }

    public void OnClickClearBtn()
    {
        m_Index = 0;
        CurrentPassCode = null;
        m_screenText.text = CurrentPassCode;
    }

    public void OnClickOkBtn()
    {
        SafeBoxMission safeBoxMissionScript = m_SafeBoxMission.GetComponent<SafeBoxMission>();

        if (safeBoxMissionScript == null)
        {
            Debug.LogError("safeBoxMissionScript is null on SafeBoxBtnManager.");
        }
        else
        {
            if (CurrentPassCode == PassCode)
            {
                Debug.Log("yay! good for you!");
                safeBoxMissionScript.ApplyPasswordCorrect();
            }
            else
            {
                Debug.Log("try again!");
                OnClickClearBtn();
                safeBoxMissionScript.ApplyPasswrodInCorrect();
            }
        }
    }
}
