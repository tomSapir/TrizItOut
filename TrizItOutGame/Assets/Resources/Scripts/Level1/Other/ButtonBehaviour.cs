using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public enum ButtonId { RoomChange, ZoomOut }

    public ButtonId m_MyID;
    private DisplayManagerLevel1 m_CurrDisplay;

    void Start()
    {
        m_CurrDisplay = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();
    }

    void Update()
    {
        hideDisplay();
        Display();
    }

    private void hideDisplay()
    {
        Color currColor = GetComponent<Image>().color;

        if ((m_CurrDisplay.CurrentState == DisplayManagerLevel1.State.normal && m_MyID == ButtonId.ZoomOut) || 
            (m_CurrDisplay.CurrentState == DisplayManagerLevel1.State.zoom && m_MyID == ButtonId.RoomChange))
        {
            GetComponent<Image>().color = new Color(currColor.r, currColor.g, currColor.b, 0);
            GetComponent<Button>().enabled = false;
            this.transform.SetSiblingIndex(0);
        }
    }  


    void Display()
    {
        Color currColor = GetComponent<Image>().color;

        if ((m_CurrDisplay.CurrentState == DisplayManagerLevel1.State.zoom && m_MyID == ButtonId.ZoomOut) ||
            (m_CurrDisplay.CurrentState == DisplayManagerLevel1.State.normal && m_MyID == ButtonId.RoomChange))
        {
            GetComponent<Image>().color = new Color(currColor.r, currColor.g, currColor.b, 1);
            GetComponent<Button>().enabled = true;
            this.transform.SetSiblingIndex(0);
        }

        if(m_CurrDisplay.CurrentState == DisplayManagerLevel1.State.busy && m_MyID == ButtonId.ZoomOut)
        {
            GetComponent<Button>().enabled = false;
        }
    }


}