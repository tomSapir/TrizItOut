using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kattle : MonoBehaviour, IInteractable
{
    [SerializeField]
    public bool m_isConnected { get; set; } // Maybe we should start unattached?

    void Start()
    {
        m_isConnected = true;
    }

    
    void Update()
    {
        
    }


    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if(m_isConnected)
        {
            Debug.Log("Show password.");
        }
        else
            Debug.Log("The koomkoom is dixconnected.");
    }
}
