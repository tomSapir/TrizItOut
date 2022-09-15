using UnityEngine;

public class Screwdriver : MonoBehaviour
{
    public GameObject m_SafeBoxOpen;
    public Sprite m_OpenSafeBoxZoomOutWithoutScrew;
    public PickUpItem m_PickUpItem;

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
