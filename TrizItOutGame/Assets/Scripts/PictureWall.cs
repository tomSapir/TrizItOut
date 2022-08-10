using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureWall : MonoBehaviour
{
    private RotateObject m_RotateObject;
    private Animation m_Animation;

    void Start()
    {
        m_RotateObject = GetComponent<RotateObject>();
        m_RotateObject.OnRotateObject += OnPictureWallRotate;
        m_Animation = GetComponent<Animation>();
    }

    public void OnPictureWallRotate()
    {
        m_Animation.Play();
    }
}
