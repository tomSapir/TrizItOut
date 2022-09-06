using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public enum eSpinDirection
    {
        right = 1,
        left = -1
    }

    public bool CanSpin;
    public eSpinDirection SpinDirection;
    public float m_SpinSpeed = 100;



    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("BandatePlaceHolder").GetComponent<PlaceHolder>().OnPrefabSpawned += onRubberSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        
         spin();
        
    }

    private void spin()
    {
        if (CanSpin)
        {
            transform.Rotate(0f, 0f, ((int)SpinDirection) * m_SpinSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void onRubberSpawn()
    {
        CanSpin = true;
    }
}
