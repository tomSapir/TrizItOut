using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private DisplayManagerLevel1 m_currDisplay;
    private MainCameraManagerLevel2 m_Level2;

    void Start()
    {
        GameObject tmp;
        tmp = GameObject.Find("DisplayImage");
        if(tmp != null)
        {
            m_currDisplay = tmp.GetComponent<DisplayManagerLevel1>();
        }
        tmp = GameObject.Find("Main Camera");
        if (tmp != null)
        {
            m_Level2 = tmp.GetComponent<MainCameraManagerLevel2>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 rayPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.zero, 100);

            if(hit && hit.transform.tag == "Interactable")
            {
                hit.transform.GetComponent<IInteractable>().Interact(m_currDisplay);
            }
        }
    }
}
