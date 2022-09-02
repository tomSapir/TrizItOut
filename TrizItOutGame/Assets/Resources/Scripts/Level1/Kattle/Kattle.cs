using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Kattle : MonoBehaviour, IInteractable
{
    public bool m_isConnected { get; set; } = false;
    public  GameObject m_KattleSmoke;
    public  GameObject m_Mirror;
    public  Sprite m_MirrorWithCodeSprite;
    public  CommunicationManagerLevel1 m_CommunicationManager;
    public  GameObject m_ButtonRightArrow;
    private GameObject m_SafeBoxClosed;

    void Start()
    {
        m_SafeBoxClosed = GameObject.Find("/Interactables_2/SafeBox/SafeBox_Closed");
        if(m_SafeBoxClosed == null)
        {
            Debug.LogError("m_SafeBoxClosed is null");
        }
        SwitchManager switchManager = GameObject.Find("/Interactables_2/Switch").GetComponent<SwitchManager>();
        switchManager.OnSwitch += OnSwitchChanged;
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if(m_isConnected)
        {
            StartCoroutine(ApplyKattleSmokeAndPassword(currDisplay));
        }
        else
        {
            m_CommunicationManager.ShowMsg("It seems you need to plug in the kettle first.");
        }
    }

    IEnumerator ApplyKattleSmokeAndPassword(DisplayManagerLevel1 currDisplay)
    {
        m_ButtonRightArrow.GetComponent<Button>().interactable = false;
        if(m_SafeBoxClosed != null)
        {
            m_SafeBoxClosed.layer = 2;
        }
        
        SoundManager.PlaySound(SoundManager.k_SwitchSoundName);
        yield return new WaitForSeconds(2);
        SoundManager.PlaySound(SoundManager.k_KattleBoilSoundName);
        yield return new WaitForSeconds(3);
        m_KattleSmoke.SetActive(true);
        yield return new WaitForSeconds(3);
        m_Mirror.GetComponent<SpriteRenderer>().sprite = m_MirrorWithCodeSprite;
        yield return new WaitForSeconds(2);
        m_KattleSmoke.SetActive(false);

        m_ButtonRightArrow.GetComponent<Button>().interactable = true;
        if (m_SafeBoxClosed != null)
        {
            m_SafeBoxClosed.layer = 0;
        }
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
