using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Hitcode_RoomEscape
{
    public class TriggerMark : MonoBehaviour
    {
        Transform Trigger;
        SpriteRenderer sp;
        public Camera myCam;
        [HideInInspector]
        public Vector3 offset;
        [HideInInspector]
        public bool useDesignOffsetPosition;
        [HideInInspector]
        public float offsetXRatio, offsetYRatio;
        public enum myEnum // your custom enumeration
        {
            topleft,
            top,
            topright,
            right,
            bottomright,
            bottom,
            bottomleft,
            left
        };
        public myEnum anchorType;

        // Use this for initialization
        void Start()
        {
            if(myCam == null)
            {
                Debug.LogException(new Exception("Trigger Mark not correctly setting yet.."), this);
                return;
            }
            Trigger = transform.parent;
            //transform.position = new Vector3(transform.position.x, transform.position.y, .1f);
            Trigger.GetComponent<SpriteRenderer>().enabled = false;

            transform.parent = myCam.transform;
            GetComponent<SpriteRenderer>().enabled = false;
            
            sp = GetComponent<SpriteRenderer>();
            sp.enabled = false;


            Vector3 v3Pos = Vector3.zero;


            Sprite mySprite;
            float pixel2units = transform.GetComponent<SpriteRenderer>().sprite.rect.width / transform.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            float tx =  transform.GetComponent<SpriteRenderer>().bounds.extents.x/2*pixel2units;
            float ty =  transform.GetComponent<SpriteRenderer>().bounds.extents.y / 2* pixel2units;
            if (useDesignOffsetPosition)
            {
                tx = 0;ty = 0;//if only use anchor point,require an base object offset,if use relative design postion,no need the object offset
            }
            //print(tx+"__"+ ty);
            switch (anchorType)
            {
                case myEnum.topleft:
                    v3Pos = new Vector3(0, Screen.height, 0) + new Vector3(tx, -ty, 0);
                    
                    break;
                case myEnum.top:
                    v3Pos = new Vector3(Screen.width / 2, Screen.height, 0) + new Vector3(0, -ty, 0);
                    break;
                case myEnum.topright:
                    v3Pos = new Vector3(Screen.width, Screen.height, 0) + new Vector3(-tx, -ty);
                    break;
                case myEnum.right:
                    v3Pos = new Vector3(Screen.width, Screen.height / 2, 0) + new Vector3(-tx, 0, 0);
                    break;
                case myEnum.bottomright:
                    v3Pos = new Vector3(Screen.width, 0, 0) + new Vector3(-tx, ty, 0);
                    break;
                case myEnum.bottom:
                    v3Pos = new Vector3(Screen.width / 2, 0, 0) + new Vector3(0, ty, 0);
                    break;
                case myEnum.bottomleft:
                    v3Pos = new Vector3(0, 0, 0) + new Vector3(tx, ty, 0);
                    break;
                case myEnum.left:
                    v3Pos = new Vector3(0, Screen.height / 2, 0)+new Vector3(tx,0,0);
                    break;
            }

            offset = new Vector3(Screen.width * offsetXRatio, Screen.height * offsetYRatio, 0);
            //print("offset" + (offset));
            if (!useDesignOffsetPosition)
            {
                transform.position = myCam.ScreenToWorldPoint(v3Pos);
            }
            else
            {
                transform.position = myCam.ScreenToWorldPoint(offset + v3Pos);
            }
           
            StartCoroutine("waitaframe");


        }

        IEnumerator waitaframe()
        {
            yield return new WaitForEndOfFrame();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (sp != null)
            {
                sp.enabled = isShow;
              
            }
            

           
        }

        bool isShow = false;
        private void OnTriggerStay2D(Collider2D collision)
        {

            //if the road mark is showed on the postion
            if(collision.gameObject == Trigger.gameObject)
            {
              
                isShow = true;
            }
            else
            {
                isShow = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
         
            //if the road mark is showed on the postion
            if (collision.gameObject == Trigger.gameObject)
            {
                isShow = false;
            }
        }

      
    }
}
