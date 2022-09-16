using UnityEngine;

public class BigPot : MonoBehaviour
{
    private MoveObject m_MoveObject;
    private Animation m_Animation;

    void Start()
    {
        m_MoveObject = GetComponent<MoveObject>();
        m_MoveObject.OnObjectMoved += OnBigPotMoved;
        m_Animation = GetComponent<Animation>();
    }

    public void OnBigPotMoved(MoveObject.eMovementType eMovementType)
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySound(SoundManager.k_MoveBigPotSoundName);

        if(eMovementType == MoveObject.eMovementType.Right)
        {
            m_Animation.Play("BigPot_Right");
        }   
        else if(eMovementType == MoveObject.eMovementType.Left)
        {
            m_Animation.Play("BigPot_Left");
        }
    }
}
