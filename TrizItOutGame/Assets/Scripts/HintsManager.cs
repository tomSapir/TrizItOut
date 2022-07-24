using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsManager : MonoBehaviour
{
    private Dictionary<int, string> m_HintsByIndex;
    private int m_CurrentHintIndex;

    [SerializeField]
    private GameObject m_HintWindow;
    [SerializeField]
    private GameObject m_ShowHintBtn;
    [SerializeField]
    private GameObject m_HintWindowText;

    // Start is called before the first frame update
    void Start()
    {
        m_HintsByIndex = new Dictionary<int, string>();
        m_CurrentHintIndex = 0;
        setHintsMapData();
    }

    // This method will fill the map with the hints and there indexes
    private void setHintsMapData()
    {
        m_HintsByIndex.Add(0, "test for checking.");
    }

    public void OnClickShowHintBtn()
    {
        if (m_HintsByIndex.ContainsKey(m_CurrentHintIndex))
        {
            m_HintWindow.SetActive(true);
            m_ShowHintBtn.SetActive(false);

            // get the current hint from the hints map
            string currentHint = m_HintsByIndex[m_CurrentHintIndex];
            m_HintWindowText.GetComponent<TMPro.TextMeshProUGUI>().text = currentHint;
        }
        else
        {
            Debug.LogError("There is no hint with index " + m_CurrentHintIndex + " In the hints map.");
        }
    }

    public void OnClickCloseHintWindow()
    {
        m_HintWindow.SetActive(false);
        m_ShowHintBtn.SetActive(true);
    }

    public void IncreaseHintIndex()
    {
        m_CurrentHintIndex++;
    }
}
