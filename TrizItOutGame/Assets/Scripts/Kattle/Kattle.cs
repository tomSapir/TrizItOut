using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kattle : MonoBehaviour, IInteractable
{
    [SerializeField]
    public bool m_isConnected { get; set; } // Maybe we should start unattached?

    [SerializeField]
    private GameObject m_KattleSmoke;

    [SerializeField]
    private GameObject m_Mirror;
    [SerializeField]
    private Sprite m_MirrorWithCodeSprite;

    void Start()
    {
        m_isConnected = true;
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if(m_isConnected)
        {
            Debug.Log("Start");
            StartCoroutine(ApplyKattleSmokeAndPassword());
         
        }
        else
        {
            Debug.Log("The koomkoom is dixconnected.");
        }
    }

    IEnumerator ApplyKattleSmokeAndPassword()
    {
        SoundManager.PlaySound(SoundManager.k_SwitchSoundName);
        yield return new WaitForSeconds(2);

        // TODO: put water boiling sound
        yield return new WaitForSeconds(2);

        m_KattleSmoke.SetActive(true);

        yield return new WaitForSeconds(3);

        m_Mirror.GetComponent<SpriteRenderer>().sprite = m_MirrorWithCodeSprite;

        yield return new WaitForSeconds(3);
        m_KattleSmoke.SetActive(false);
    }
}
