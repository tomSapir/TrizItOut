using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSUManager : MonoBehaviour
{
    public delegate void PsuMissionSolvedDelegate();

    private HashSet<int> m_CurrentPapersClip = new HashSet<int>();
    private readonly HashSet<int> m_SolutionPapersClip = new HashSet<int>() { 0, 5, 6, 7, 8, 13, 14, 15, 17, 27 };

    public event PsuMissionSolvedDelegate PsuMissionSolved;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<PaperClipPlaceholderManager>().PaperClipPressed += PaperClip_Pressed;
            transform.GetChild(i).gameObject.GetComponent<PaperClipPlaceholderManager>().m_Id = i;
        }
    }

    public void PaperClip_Pressed(int i_Id)
    {
        addOrRemovePaperClip(i_Id);
        if (m_CurrentPapersClip.Count == m_SolutionPapersClip.Count)
        {
            if(m_CurrentPapersClip.SetEquals(m_SolutionPapersClip))
            {
                turnOffChilds();
                PsuMissionSolved?.Invoke();
            }
        }
    }

    private void turnOffChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = 2;
        }
    }

    private void addOrRemovePaperClip(int i_Id)
    {
        if (m_CurrentPapersClip.Contains(i_Id))
        {
            m_CurrentPapersClip.Remove(i_Id);
        }
        else
        {
            m_CurrentPapersClip.Add(i_Id);
        }
    }
}
