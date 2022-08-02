using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : MonoBehaviour
{

    [SerializeField]
    private GameObject m_SafeBoxOpen;

    [SerializeField]
    private Sprite m_OpenSafeBoxZoomOutWithoutScrew;

    [SerializeField]
    private PickUpItem m_PickUpItem;

    // Start is called before the first frame update
    void Start()
    {
        m_PickUpItem = GetComponent<PickUpItem>();
        m_PickUpItem.OnPickUp += OnScrewdriverPickedUp;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnScrewdriverPickedUp()
    {
        Debug.Log("Event works!");

        m_SafeBoxOpen.GetComponent<SpriteRenderer>().sprite = m_OpenSafeBoxZoomOutWithoutScrew;
    }


}
