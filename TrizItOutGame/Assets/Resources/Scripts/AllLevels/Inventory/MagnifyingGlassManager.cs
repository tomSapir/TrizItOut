using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnifyingGlassManager : MonoBehaviour, IInteractable
{
    public Sprite m_ZoomBtnHighlightSprite;
    public Sprite m_ZoomBtnNormaltSprite;
    public GameObject m_ZoomBtn;

    public delegate void ImageWasClickedDelegate();
    public event ImageWasClickedDelegate ImageWasClickedHandler;

    void Start()
    {
       // ImageWasClickedHandler += gameObject.transform.parent.GetComponent<SlotManager>().OnClickMagnifierGlass;
    }


    void Update()
    {
        
    }

    public void HighlightWhenEnter()
    {
        m_ZoomBtn.GetComponent<Image>().sprite = m_ZoomBtnHighlightSprite;
    }

    public void RemoveHighlightWhenEnter()
    {
        m_ZoomBtn.GetComponent<Image>().sprite = m_ZoomBtnNormaltSprite;
    }

    public void OnClickMagnifierGlass()
    {
       // ImageWasClickedHandler?.Invoke();
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        m_ZoomBtn.GetComponent<Button>().onClick?.Invoke();
    }
}
