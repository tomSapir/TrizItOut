using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private DisplayManagerLevel1 m_CurrentDisplay;

    [SerializeField]
    private Button m_RightButton;
    [SerializeField]
    private Button m_LeftButton;
    [SerializeField]
    private Button m_ZoomOutButton;

    [SerializeField]
    private GameObject m_ZoomWindow;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentDisplay = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();

        if (m_CurrentDisplay == null)
        {
            Debug.LogError("Could not found DisplayManagerLevel1.");
        }
    }



    public void OnClickRightArrow()
    {
        // increase the current display index by one (automaticly change the display at update method in DisplayManagerLevel1)
       
        m_CurrentDisplay.CurrentWall++;
        m_LeftButton.gameObject.SetActive(true);
        m_RightButton.gameObject.SetActive(false);
       
    }  

    public void OnClickLeftArrow()
    {
        // decrease the current display index by one (automaticly change the display at update method in DisplayManagerLevel1)
        
        m_CurrentDisplay.CurrentWall--;
        m_LeftButton.gameObject.SetActive(false);
        m_RightButton.gameObject.SetActive(true);

    }

    public void OnClickCloseZoomInventory()
    {
        m_ZoomWindow.SetActive(false);
    }

    public void OnClickZoomReturn()
    {
        
        m_CurrentDisplay = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();
        m_CurrentDisplay.CurrentState = DisplayManagerLevel1.State.normal;

        m_CurrentDisplay.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Level1/Background" + m_CurrentDisplay.CurrentWall.ToString());

    }
}
