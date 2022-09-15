using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public enum ButtonId { roomChange, zoomOut }

    public ButtonId m_MyID;
    private DisplayManagerLevel1 m_CurrDisplay;

    void Start()
    {
        m_CurrDisplay = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();
    }

    void Update()
    {
        HideDisplay();
        Display();
    }

    void HideDisplay()
    {
        if(m_CurrDisplay.CurrentState == DisplayManagerLevel1.State.normal && m_MyID == ButtonId.zoomOut)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
                GetComponent<Image>().color.b, 0);

            GetComponent<Button>().enabled = false;
            this.transform.SetSiblingIndex(0);
        }

        if (m_CurrDisplay.CurrentState == DisplayManagerLevel1.State.zoom && m_MyID == ButtonId.roomChange)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
                GetComponent<Image>().color.b, 0);

            GetComponent<Button>().enabled = false;
            this.transform.SetSiblingIndex(0);
        }
    }  

    void Display()
    {
        if (m_CurrDisplay.CurrentState == DisplayManagerLevel1.State.zoom && m_MyID == ButtonId.zoomOut)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
                GetComponent<Image>().color.b, 1);

            GetComponent<Button>().enabled = true;
            this.transform.SetSiblingIndex(0);
        }

        if (m_CurrDisplay.CurrentState == DisplayManagerLevel1.State.normal && m_MyID == ButtonId.roomChange)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
                GetComponent<Image>().color.b, 1);

            GetComponent<Button>().enabled = true;
            this.transform.SetSiblingIndex(0);
        }

        if(m_CurrDisplay.CurrentState == DisplayManagerLevel1.State.busy && m_MyID == ButtonId.zoomOut)
        {
            GetComponent<Button>().enabled = false;
        }
    }


}