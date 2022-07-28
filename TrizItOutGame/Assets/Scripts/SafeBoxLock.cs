using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SafeBoxLock : MonoBehaviour
{
    string PassCode = "1234";
    string CurrentPassCode = null;
    private int Index = 0 ;
    [SerializeField]
    public TextMeshProUGUI uiText;



    public void CodeFunction(string Number)
    {
        if (Index < 4)
        {
            Index++;
            CurrentPassCode = CurrentPassCode + Number;
            uiText.text = CurrentPassCode;
        }
    }

    public void CheckPassCode()
    {
        if(CurrentPassCode == PassCode)
        {
            Debug.Log("yay! good for you!");
        }
        else
        {
            Debug.Log("try again!");
            ResetPassCode();
        }
    }

    public void ResetPassCode()
    {
        Index = 0;
        CurrentPassCode = null;
        uiText.text = CurrentPassCode;
    }

}
