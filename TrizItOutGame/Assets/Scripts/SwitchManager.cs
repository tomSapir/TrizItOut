using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Sprite m_SwitchOnSprite;
    [SerializeField]
    private Sprite m_SwitchOffSprite;

    [SerializeField]
    private GameObject m_Dark;

    private bool m_IsLightOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if(m_IsLightOn)
        {
            GetComponent<SpriteRenderer>().sprite = m_SwitchOffSprite;
            m_Dark.SetActive(true);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = m_SwitchOnSprite;
            m_Dark.SetActive(false);
        }

        m_IsLightOn = !m_IsLightOn;
    }
}
