using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsManager : MonoBehaviour
{
    private Dictionary<int, string> m_HintsMap;
    private int m_CurrentHintIndex;

    [SerializeField]
    private GameObject m_HintWindow;
    [SerializeField]
    private GameObject m_HintBtn;
    [SerializeField]
    private GameObject m_HintText;

    // Start is called before the first frame update
    void Start()
    {
        m_HintsMap = new Dictionary<int, string>();
        m_CurrentHintIndex = 0;
        setHintsMapData();

        // TODO: add all the hints to "Hints"
    }

    private void setHintsMapData()
    {
        // TODO: this method will fill the map with the hints and there indexes
        m_HintsMap.Add(0, "test for checking.");
    }

    public void OnClickHintBtn()
    {
        if (m_HintsMap.ContainsKey(m_CurrentHintIndex))
        {
            m_HintWindow.SetActive(true);
            m_HintBtn.SetActive(false);

            // get the current hint from Hints map
            string currentHint = m_HintsMap[m_CurrentHintIndex];
            m_HintText.GetComponent<TMPro.TextMeshProUGUI>().text = currentHint;
        }
        else
        {
            Debug.LogError("There is no hint with index " + m_CurrentHintIndex + " In the hints map.");
        }
    }

    public void OnClickCloseHintWindow()
    {
        m_HintWindow.SetActive(false);
        m_HintBtn.SetActive(true);
    }
}
