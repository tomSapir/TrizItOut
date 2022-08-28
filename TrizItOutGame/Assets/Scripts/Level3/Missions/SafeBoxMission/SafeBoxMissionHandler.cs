using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SafeBoxMissionHandler : MonoBehaviour
{
    private HashSet<int> m_currentSolution = new HashSet<int>();
    private readonly HashSet<int> m_Solution = new HashSet<int>() {1,7,8,11,12,3};

    public delegate void SafeBoxOpenedDelegate();
    public event SafeBoxOpenedDelegate SafeBoxOpened;

    public GameObject m_Indicator;
    public Sprite m_SafeBoxOpened;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnClick()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        Image image = button.GetComponent<Button>().image;
        image.color = new Color32(217, 91, 255, 152);
        m_currentSolution.Add(int.Parse(button.name));
    }

    public void SubmitSolution()
    {
        Debug.Log("enterd");
        if (m_currentSolution == m_Solution)
        {
            m_Indicator.GetComponent<Image>().color = new Color32(12, 255, 0, 255);
            GameObject.Find("SafeBoxMission").GetComponent<SpriteRenderer>().sprite = m_SafeBoxOpened;
            SafeBoxOpened?.Invoke();
        }
        else
        {
            m_Indicator.GetComponent<Image>().color = new Color32(255, 0, 71, 255);
            m_currentSolution.Clear();
            //TODO: TRASFORM ALL BUTTONS COLORS TO WHITE AGAIN
        }
    }
}
