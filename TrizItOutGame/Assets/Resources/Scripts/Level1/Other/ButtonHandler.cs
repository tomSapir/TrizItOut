using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private DisplayManagerLevel1 m_CurrentDisplay;

    public Button m_RightButton;
    public Button m_LeftButton;
    public Button m_ZoomOutButton;
    public GameObject m_ZoomWindow;

    private const string k_BackgroundPath = "Sprites/Level1/Main_Backgrounds/Background";

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
        m_CurrentDisplay.ChangeToNormalBackgroundAfterReturnFromZoom();
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }
}
