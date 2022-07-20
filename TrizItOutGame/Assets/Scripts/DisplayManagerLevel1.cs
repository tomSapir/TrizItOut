using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManagerLevel1 : MonoBehaviour
{
    // index for the current background displayed (from 1 to 2 in this case)
   
    private int m_CurrentWall;

    private int m_PreviousWall;

    [SerializeField]
    private GameObject m_furniture1;
    [SerializeField]
    private GameObject m_interactables1;

    public enum State
    {
        normal, zoom
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
        CurrentState = State.normal;
    }

    void Update()
    {
        if (m_CurrentWall != m_PreviousWall)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Level1/Background" + CurrentWall.ToString());
        }

        m_PreviousWall = CurrentWall;

        showRelevantPickUpItems();
        showRelevantInteractableItems();
    }

    private void showRelevantInteractableItems()
    {
        if (m_CurrentWall == 1 && CurrentState == State.normal)
        {
            m_interactables1.SetActive(true);
        }
        else if (m_CurrentWall == 2 || CurrentState == State.zoom)
        {
            m_interactables1.SetActive(false);
        }
    }

    private void showRelevantPickUpItems()
    {
        if(m_CurrentWall == 1 && CurrentState == State.normal)
        {
            m_furniture1.SetActive(true);
        }
        else if(m_CurrentWall == 2 || CurrentState == State.zoom)
        {
            m_furniture1.SetActive(false);
        }
    }
}
