using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Sprite m_SwitchOnSprite;

    [SerializeField]
    private GameObject m_Dark;

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
        GetComponent<SpriteRenderer>().sprite = m_SwitchOnSprite;
        m_Dark.SetActive(false);
    }
}
