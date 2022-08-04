using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float m_SpeedRotation;
    [SerializeField]
    private Vector3 m_EndPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        RotateSprite();
    }

    private void RotateSprite()
    {
        while (transform.position != m_EndPoint)
        {
            transform.Rotate(Vector3.forward * m_SpeedRotation * Time.deltaTime);
        }
    }
}
