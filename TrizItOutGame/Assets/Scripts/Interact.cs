using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private DisplayManagerLevel1 m_currDisplay;

    // Start is called before the first frame update
    void Start()
    {
        m_currDisplay = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();
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
                hit.transform.GetComponent<Interactable>().Interact(m_currDisplay);
            }
        }
    }
}
