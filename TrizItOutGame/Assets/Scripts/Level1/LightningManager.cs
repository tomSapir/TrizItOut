using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public GameObject m_Lightning1, m_Lightning2;
    public GameObject m_ComputerCable;

    // Start is called before the first frame update
    void Start()
    {
        m_ComputerCable.GetComponent<PickUpItem>().OnPickUp += OnTornComputerCablePickedUp;
    }


    public void OnTornComputerCablePickedUp()
    {
        m_Lightning1.SetActive(false);
        m_Lightning2.SetActive(false);
    }
}
