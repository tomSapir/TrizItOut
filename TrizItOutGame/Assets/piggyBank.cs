using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piggyBank : MonoBehaviour, IInteractable
{
    private static int s_AmountOfTaps = 0;
    [SerializeField]
    private Sprite m_PiggyBankBroken;
    [SerializeField]
    private GameObject m_TrizCoin;
    int AmoutOfTaps { get; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        s_AmountOfTaps++;
        if(s_AmountOfTaps == 3)
        {
            GetComponent<SpriteRenderer>().sprite = m_PiggyBankBroken;
        }
        else if(s_AmountOfTaps == 4)
        {

            GameObject inventory = GameObject.Find("/Canvas/Inventory");
            if(inventory != null)
            {
                GameObject trizCoin = Instantiate(m_TrizCoin);
                trizCoin.GetComponent<PickUpItem>().Interact(currDisplay);

            }
        }
    }
}
