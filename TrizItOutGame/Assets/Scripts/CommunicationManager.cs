using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommunicationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CommunicationWindow;
    [SerializeField]
    private GameObject m_CommunicationText;

    // Start is called before the first frame update
    void Start()
    {
        ComputerMission computerMission = GameObject.Find("/Missions/Computer_Mission").GetComponent<ComputerMission>();
        computerMission.OnComputer += onComputerChanged;
    }

    public void ShowMsg(string i_Msg)
    {
        StartCoroutine(ShowMsgEnumerator(i_Msg));
    }

    IEnumerator ShowMsgEnumerator(string i_Msg)
    {
        m_CommunicationWindow.SetActive(true);
        m_CommunicationText.SetActive(true);
        m_CommunicationText.GetComponent<TextMeshProUGUI>().text = i_Msg;
        yield return new WaitForSeconds(4);
        m_CommunicationWindow.SetActive(false);
        m_CommunicationText.SetActive(false);
    }

    public void onComputerChanged()
    {
        ShowMsg("well,it's seems like it is open now, nice one!");
    }
}
