using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneHandHandler : MonoBehaviour
{
    private float m_MovementSpeed = 1.5f;
    public bool CanMove;

    void Update()
    {
        manageMovement();
    }

    void Start()
    {
        GameObject.Find("BandatePlaceHolder").GetComponent<PlaceHolder>().OnPrefabSpawned += onRubberSpawned;
    }

    private void manageMovement()
    {
        if(MainCameraManagerLevel3.m_CurrentWallIndex == 3 && CanMove)
        {
            float movement = Input.GetAxis("Horizontal");
            Vector3 newPosition = transform.position + new Vector3(movement, 0, 0) * Time.deltaTime * m_MovementSpeed;

            if (newPosition.x >= 29 && newPosition.x <= 38.36)
            {
                transform.position = newPosition;
            }
        }
    }

    private void onRubberSpawned()
    {
        CanMove = true;
    }

}
