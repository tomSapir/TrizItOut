using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Kattle : MonoBehaviour, IInteractable
{
    [SerializeField]
    public bool m_isConnected { get; set; }

    [SerializeField]
    private GameObject m_KattleSmoke;
    [SerializeField]
    private GameObject m_Mirror;
    [SerializeField]
    private Sprite m_MirrorWithCodeSprite;
    [SerializeField]
    private GameObject m_CommunicationInterface;

    private GameObject m_ButtonRightArrow;
    private GameObject m_SafeBoxClosed;

    void Start()
    {
        m_isConnected = true;
        m_ButtonRightArrow = GameObject.Find("/Canvas/Arrow_Right_Btn");
        m_SafeBoxClosed = GameObject.Find("/interactables2/SafeBox/SafeBox_Closed");
        SwitchManager switchManager = GameObject.Find("/interactables2/Switch").GetComponent<SwitchManager>();
        switchManager.OnSwitch += OnSwitchChanged;
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if(m_isConnected)
        {
            Debug.Log("Start");
            StartCoroutine(ApplyKattleSmokeAndPassword(currDisplay));
        }
        else
        {
            m_CommunicationInterface.GetComponent<CommunicationManager>().ShowMsg("It seems you need to plug in the kettle first.");
        }
    }

    IEnumerator ApplyKattleSmokeAndPassword(DisplayManagerLevel1 currDisplay)
    {
        m_ButtonRightArrow.GetComponent<Button>().interactable = false;
        m_SafeBoxClosed.layer = 2;

        SoundManager.PlaySound(SoundManager.k_SwitchSoundName);
        yield return new WaitForSeconds(2);
        SoundManager.PlaySound(SoundManager.k_KattleBoilSoundName);
        yield return new WaitForSeconds(4);
        m_KattleSmoke.SetActive(true);
        yield return new WaitForSeconds(3);
        m_Mirror.GetComponent<SpriteRenderer>().sprite = m_MirrorWithCodeSprite;
        yield return new WaitForSeconds(6);
        m_KattleSmoke.SetActive(false);

        m_ButtonRightArrow.GetComponent<Button>().interactable = true;
        m_SafeBoxClosed.layer = 0;

    }

    public void OnSwitchChanged(bool i_IsOn)
    {
        if (i_IsOn)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 2;
        }
    }
}
