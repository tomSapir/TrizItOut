using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMission : MonoBehaviour
{
    private DisplayManagerLevel1 m_DisplayManager;

    [SerializeField]
    private GameObject[] m_Dusts;

    [SerializeField]
    private Sprite m_PCSideOpenSprite;

    private bool m_CanShowDust = true;
    
   
    void Start()
    {
        m_DisplayManager = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();

        if(m_DisplayManager == null)
        {
            Debug.LogError("DisplayManagerLevel1 in ComputerMission is null!");
        }
    }


    void Update()
    {
        ChangeImage();
    }

    private void ChangeImage()
    {
        if (GameObject.Find("/Missions/Computer_Mission/Screw") == null)
        {
            m_DisplayManager.GetComponent<SpriteRenderer>().sprite = m_PCSideOpenSprite;

            if (m_CanShowDust)
            {
                foreach (GameObject dust in m_Dusts)
                {
                    dust.SetActive(true);
                }

                m_CanShowDust = false;
            }
        }
    }
}
