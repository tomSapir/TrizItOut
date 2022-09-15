using UnityEngine;
using UnityEngine.UI;

public class MagnifyingGlassManager : MonoBehaviour, IInteractable
{
    public Sprite m_ZoomBtnHighlightSprite;
    public Sprite m_ZoomBtnNormaltSprite;
    public GameObject m_ZoomBtn;

    public void HighlightWhenEnter()
    {
        m_ZoomBtn.GetComponent<Image>().sprite = m_ZoomBtnHighlightSprite;
    }

    public void RemoveHighlightWhenEnter()
    {
        m_ZoomBtn.GetComponent<Image>().sprite = m_ZoomBtnNormaltSprite;
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        m_ZoomBtn.GetComponent<Button>().onClick?.Invoke();
    }
}
