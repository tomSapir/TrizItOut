using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
    public class SafeBoxMissionHandler : MonoBehaviour
{
    private HashSet<int> m_currentSolution = new HashSet<int>();
    private readonly HashSet<int> m_Solution = new HashSet<int>() { };

    public GameObject indicator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        Image image = button.GetComponent<Button>().image;
        image.color = new Color32(217, 91, 255,152);
        m_currentSolution.Add(int.Parse(button.name));
    }

    private void submitSolution()
    {
        if(m_currentSolution == m_Solution)
        {
            indicator.GetComponent<Image>().color = new Color32(26, 97, 0, 255);
        }
        else
        {
            indicator.GetComponent<Image>().color = new Color32(203, 0, 0, 163);
        }
    }
}
