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

    [SerializeField]
    private GameObject m_SwitchManager;

    string PassCode = "7135";
    string CurrentPassCode = null;
    private int m_Index = 0;

    public void Start()
    {
        m_SwitchManager.GetComponent<SwitchManager>().OnSwitch += OnSwitchChanged;
    }

    public void OnClickSafeBoxNumberBtn(string Number)
    {
        SoundManager.PlaySound(SoundManager.k_ButtonSoundName);
        if (m_Index < 4)
        {
            m_Index++;
            CurrentPassCode = CurrentPassCode + Number;
            m_screenText.text = CurrentPassCode;
        }
    }

    public void OnClickClearBtn()
    {
        SoundManager.PlaySound(SoundManager.k_ButtonSoundName);
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
                SoundManager.PlaySound(SoundManager.k_CorrectPasswordSoundName);
                safeBoxMissionScript.ApplyPasswordCorrect();
            }
            else
            {
                SoundManager.PlaySound(SoundManager.k_WorngPasswordSoundName);
                OnClickClearBtn();
                safeBoxMissionScript.ApplyPasswrodInCorrect();
            }
        }
    }

    public void OnSwitchChanged(bool i_IsOn)
    {
        Debug.Log("YES");
        for(int i = 0; i < transform.childCount; i++)
        {
           //if(transform.GetChild(i).GetComponent<Button>() == null)
            //{
                //Debug.Log("transform.GetChild(i).gameObject.GetComponent<Button>() is null");
            //}

            transform.GetChild(i).gameObject.GetComponent<Button>().enabled = i_IsOn;
        }
    }
}
