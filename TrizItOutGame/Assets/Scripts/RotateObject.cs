using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float m_SpeedRotation;
    [SerializeField]
    private float m_EndZ;

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
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z = 32;  //this number is the degree of rotation around Z Axis
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
