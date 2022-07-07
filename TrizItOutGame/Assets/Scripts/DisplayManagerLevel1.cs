using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManagerLevel1 : MonoBehaviour
{
    // index for the current background displayed (from 1 to 2 in this case)
    private int m_CurrentWall;
    private int m_PreviousWall;

    private const string k_BackgroundsSpritesPath = "Sprites/Level1/Background";

    [SerializeField]
    private GameObject m_Furnitures1;

    public enum State
    {
        normal, zoom, ChangedView
    };

    public State CurrentState { get; set; }

    public int CurrentWall
    {
        get { return m_CurrentWall; }
        set
        {
            if (value == 3)
            {
                m_CurrentWall = 1; 
            }
            else if (value == 0)
            {
                m_CurrentWall = 2;
            }
            else
            {
                m_CurrentWall = value;
            }
        }
    }

    void Start()
    {
        m_PreviousWall = 0;
        m_CurrentWall = 1;
    }

    void Update()
    {
        if(m_CurrentWall != m_PreviousWall)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(k_BackgroundsSpritesPath + CurrentWall.ToString());
        }

        m_PreviousWall = CurrentWall;
        showRelevantPickUpItems();
    }

    private void showRelevantPickUpItems()
    {
        if(m_CurrentWall == 1)
        {
            m_Furnitures1.SetActive(true);
        }
        else if(m_CurrentWall == 2)
        {
            m_Furnitures1.SetActive(false);
        }
    }
}
