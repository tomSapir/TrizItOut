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

    public void OnScrewdriverPickedUp()
    {
        m_SafeBoxOpen.GetComponent<SpriteRenderer>().sprite = m_OpenSafeBoxZoomOutWithoutScrew;
    }
}
