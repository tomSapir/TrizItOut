using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public enum ButtonId
    {
        roomChange, zoomOut
    }

    public ButtonId m_myID;
    private DisplayManagerLevel1 currDisplay;

    void Start()
    {
        currDisplay = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();
    }

    void Update()
    {
        HideDisplay();
        Display();
    }

    void HideDisplay()
    {
        if(currDisplay.CurrentState == DisplayManagerLevel1.State.normal && m_myID == ButtonId.zoomOut)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
                GetComponent<Image>().color.b, 0);

            GetComponent<Button>().enabled = false;
            this.transform.SetSiblingIndex(0);
        }

        if (currDisplay.CurrentState == DisplayManagerLevel1.State.zoom && m_myID == ButtonId.roomChange)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
                GetComponent<Image>().color.b, 0);

            GetComponent<Button>().enabled = false;
            this.transform.SetSiblingIndex(0);
        }
    }  

    void Display()
    {
        if (currDisplay.CurrentState == DisplayManagerLevel1.State.zoom && m_myID == ButtonId.zoomOut)
        {

            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
                GetComponent<Image>().color.b, 1);

            GetComponent<Button>().enabled = true;
            this.transform.SetSiblingIndex(0);
        }

        if (currDisplay.CurrentState == DisplayManagerLevel1.State.normal && m_myID == ButtonId.roomChange)
        {
            GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g,
                GetComponent<Image>().color.b, 1);

            GetComponent<Button>().enabled = true;
            this.transform.SetSiblingIndex(0);
        }

        if(currDisplay.CurrentState == DisplayManagerLevel1.State.busy && m_myID == ButtonId.zoomOut)
        {
            GetComponent<Button>().enabled = false;
        }
    }


}