using System.Collections;
using System.Collections.Generic;
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
        SoundManager.PlaySound(SoundManager.k_MoveBigPotSoundName);

        if(eMovementType == MoveObject.eMovementType.right)
        {
            m_Animation.Play("BigPot_Right");
        }   
        else if(eMovementType == MoveObject.eMovementType.left)
        {
            m_Animation.Play("BigPot_Left");
        }
    }
}
