using System;
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
    [SerializeField]
    private GameObject m_TornFuze;

    [SerializeField]
    private GameObject m_PowerFuzeContainer;

    private bool m_CanShowDust = true;
    private int m_AmountOfDust;

    void Start()
    {
        m_DisplayManager = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();

        if (m_DisplayManager == null)
        {
            Debug.LogError("DisplayManagerLevel1 in ComputerMission is null!");
        }

        m_AmountOfDust = m_Dusts.Length;
        subscribeToDustCleanUp();
        m_TornFuze.GetComponent<PickUpItem>().OnPickUp += activeFuzeContainer;
    }

    private void subscribeToDustCleanUp()
    {
        foreach(GameObject dust in m_Dusts)
        {
            dust.GetComponent<Dust>().OnCleanUp += SetFuseOn;
        }
    }

    private void activeFuzeContainer()
    {
        m_PowerFuzeContainer.layer = 0;
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
                m_TornFuze.SetActive(true);
                m_CanShowDust = false;
            }
        }
    }

    public void SetFuseOn()
    {
        m_AmountOfDust--;
        if(m_AmountOfDust == 0)
        {
            m_TornFuze.gameObject.layer = 0;
        }
    }
}