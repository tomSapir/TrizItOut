using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private string m_Name = string.Empty;
    [SerializeField]
    private Sprite m_Image = null;
    [SerializeField]
    private bool m_InInventory = false;

    public void OnClick()
    {
        
    }

    
    public string Name
    {
        get { return m_Name; }
        set { m_Name = value; }
    }
    

    public Sprite Image
    {
        get { return m_Image; }
        set { m_Image = value; }
    }

    public bool InInventory
    {
        get { return m_InInventory; }
        set { m_InInventory = value; }
    }
}
