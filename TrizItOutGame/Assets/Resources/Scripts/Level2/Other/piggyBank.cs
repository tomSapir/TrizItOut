using UnityEngine;

public class piggyBank : MonoBehaviour, IInteractable
{
    public static int s_AmountOfTaps = 0;
    public Sprite m_PiggyBankBroken;
    public GameObject m_TrizCoin;
    public int AmoutOfTaps { get; }

    void Start()
    {
        s_AmountOfTaps = 0;
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        SoundManager soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        s_AmountOfTaps++;
        if(s_AmountOfTaps < 3)
        {
            soundManager.PlaySound(SoundManager.k_PiggyBankKnockSoundName);
        }
        else if(s_AmountOfTaps == 3)
        {
            GetComponent<SpriteRenderer>().sprite = m_PiggyBankBroken;
            soundManager.PlaySound(SoundManager.k_PiggyBankBreakSoundName);
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
