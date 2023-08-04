using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
namespace Hitcode_RoomEscape
{


    public enum operators
    {
        Equal,
        NotEqual,
        Greator,
        Less
        
    }

    public enum interactiveTypes
    {
        condition,
        playFailAction,
        playSuccessAction,
        comment
    }

    public enum itemOP
    {
        isItem,
        notItem
    }





    [System.Serializable]
    public class InteractiveObject : MonoBehaviour

    {


        public List<Interactive> interactives;
        int instanceId = 0;
        void OnEnable()
        {

            StartCoroutine("waitAFrame");

        }


        IEnumerator waitAFrame()
        {
            yield return new WaitForEndOfFrame();
            Actions tactions = GetComponent<Actions>();
            if (tactions != null)
            {
                instanceId = tactions.actionsId;

            }

            for (int i = 0; i < interactives.Count; i++)
            {
                if (interactives[i].autoCheck)
                {
                    checkEachInteractive(i);
                }
            }
        }





        Vector3 touchPosWorld;
        //Change me to change the touch phase used.
        //TouchPhase touchPhase = TouchPhase.Began;
        // Update is called once per frame
        bool mouseDown = false;
        GameObject currentTouchObject = null;
        public bool isUIOverride { get; private set; }
        void Update()
        {

            //#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
            //            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            //        {
            //            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            //            {
            //            checkTouch();
            //            }
            //        }
            //#else
            //            if (Input.GetMouseButtonUp(0))
            //            {
            //                if (!EventSystem.current.IsPointerOverGameObject())
            //                {
            //                    checkTouch();
            //                }
            //            }
            //#endif






            //        if (InputHelper.GetTouches().Count > 0 && InputHelper.GetTouches()[0].phase == TouchPhase.Ended)
            //{
            //    if (!EventSystem.current.IsPointerOverGameObject(InputHelper.GetTouches()[0].fingerId))
            //    {

            //        checkTouch();

            //    }
            //}



            isUIOverride = EventSystem.current.IsPointerOverGameObject();
        }
        
        void OnMouseDown()
        {
            if (currentTouchObject == null)
            {
                currentTouchObject = gameObject;
            }
        }
        private void OnMouseUp()
        {
            if (currentTouchObject == gameObject)
            {
                checkTouch();
                currentTouchObject = null;
            }
            else
            {
                currentTouchObject = null;
            }
        }

        void checkTouch()
        {
            //if (GameData.Instance.cameraList == null || GameData.Instance.cameraList.Count == 0) return;
            //Camera _tcam = GameData.Instance.cameraList[GameData.Instance.cameraList.Count - 1];
            //if (_tcam == null) return;
            //if (GameData.Instance.currentSubCam != null)
            //{
            //    _tcam = GameData.Instance.currentSubCam;
            //}
           
            ////We transform the touch position into word space from screen space and store it.
            //touchPosWorld = _tcam.ScreenToWorldPoint(InputHelper.GetTouches()[0].position);
            ////print(_tcam.name);
            //Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            ////We now raycast with this information. If we have hit something we can process it.
            //RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, _tcam.transform.forward);//Camera.main.transform.forward);

            //if (hitInformation.collider != null)
            //{
            //    //We should have hit something with a 2D Physics collider!
            //    GameObject touchedObject = hitInformation.transform.gameObject;
            //    //touchedObject should be the object someone touched.

            //    //print(touchedObject.name);
            //    //if you are being touched
            //    if (touchedObject == gameObject)
            //    {

                    if (GameData.Instance.locked) return;
                    //clear ui text first
                    //GameObject.Find("UItipText").GetComponent<Text>().text = "";
                    //for get which camera is object belong
                    planes = GeometryUtility.CalculateFrustumPlanes(GameData.Instance.cameraList[GameData.Instance.cameraList.Count - 1]);

                    bool canCheck = true;//whether touch is available
                    if (EventSystem.current.IsPointerOverGameObject())//is using UI(example: on item panel)
                    {
                        //canCheck = false;
                    }
                    else//not using ui menus
                    {
                        if (GameData.Instance.currentSubCam != null)//have modal scene at top
                        {
                            //if (IsVisibleInCam(gameObject))//object in base camera,should not available now
                            //{
                            //    canCheck = false;
                            //}
                            //else
                            //{

                            //    //not in a specific additional top cameara,can check
                            //    canCheck = true;

                            //}

                        }
                        if (canCheck)
                        {

                            //if is an triger mark,only avaible on visible.
                            if (transform.GetComponent<TriggerMark>() != null)
                            {
                                if (gameObject.GetComponent<SpriteRenderer>().enabled)
                                {
                                    canCheck = true;
                                }
                                else
                                {
                                    canCheck = false;//not visible,not check
                                }

                            }
                            else
                            {
                                canCheck = true;
                            }

                            if (canCheck)
                            {
                                for (int i = 0; i < interactives.Count; i++)
                                {
                                    if (interactives[i].autoCheck) continue;//ignore autochecks when manual touch

                                    try
                                    {
                                        checkEachInteractive(i);//if (checkEachInteractive(i)) continue;
                                    }
                                    catch (System.ArgumentException e)
                                    {
                                        throw new Exception("no such action index!");
                                    }
                                }
                            }
                        }
                    }
                //}




            //}
        }

        private Plane[] planes;
 
        



        bool IsVisibleInCam(GameObject go)
        {
            return GeometryUtility.TestPlanesAABB(planes, go.GetComponent<BoxCollider2D>().bounds);
        }




        bool checkEachInteractive(int tIndex)
        {
            bool breakedInSwitch = false;
            bool playThisAction = false;//if the following check all passed play actions of this interactive;
            bool isUsingItem = false;
            for (int j = 0; j < interactives[tIndex].conditions.Count; j++)
            {
                if (breakedInSwitch) break;
                Condition tcondition = interactives[tIndex].conditions[j];
                //check if item is on use
                if (tcondition.currentItem.Trim() != "")//not check if leave blank
                {
                    if (tcondition.usingItem == itemOP.isItem)//checking if using the right item
                    {
                        if (GameData.Instance.currentItem != tcondition.currentItem)
                        {
                            //not using a required item
                            playThisAction = false;
                            break;//can not play this action
                        }
                        else
                        {
                            isUsingItem = true;
                            playThisAction = true;//just can play now,can be nagetive in later check
                        }
                    }
                    else//checking if using the wrong item
                    {
                        if (GameData.Instance.currentItem == tcondition.currentItem)
                        {
                            //not using a required item
                            playThisAction = false;
                            break;//can not play this action
                        }
                        else
                        {
                            isUsingItem = true;
                            playThisAction = true;//just can play now,can be nagetive in later check
                        }
                    }
                }
                else
                {
                    playThisAction = true;//blank means pass.but can be nagetive in later check
                }



                string tname = tcondition.stateName;
                int currentValue = 0;
                int requireValue = 0;


                if (tname.Trim() != "")//only check when there is such a 'state value' exist to check;//PlayerPrefs.HasKey(tname)
                {

                    currentValue = PlayerPrefs.GetInt(tname + 0);
                    requireValue = tcondition.stateValue;



                    switch (tcondition.op)
                    {
                        case operators.Greator:
                            if (currentValue > requireValue)
                            {
                                playThisAction = true;//just can play now,can be nagetive in later check
                            }
                            else
                            {
                                playThisAction = false;
                                breakedInSwitch = true ;
                            }
                            break;
                        case operators.Less:
                            if (currentValue < requireValue)
                            {
                                playThisAction = true;//just can play now,can be nagetive in later check

                            }
                            else
                            {
                                playThisAction = false;
                                breakedInSwitch = true;
                            }
                            break;
                        case operators.NotEqual:
                            if (currentValue != requireValue)
                            {
                                playThisAction = true;//just can play now,can be nagetive in later check

                            }
                            else
                            {
                                playThisAction = false;
                                breakedInSwitch = true;
                            }
                            break;
                        case operators.Equal:
                            if (currentValue == requireValue)
                            {
                                playThisAction = true;//just can play now,can be nagetive in later check

                            }
                            else
                            {
                                playThisAction = false;
                                breakedInSwitch = true;
                            }
                            break;
                    }
                }
            }


            //if no condition
            if (interactives[tIndex].conditions == null || interactives[tIndex].conditions.Count == 0)
            {
                playThisAction = true;//all condition passed,really can play
            }

            //if all blank
            bool allblank = true;
            for (int j = 0; j < interactives[tIndex].conditions.Count; j++)
            {

                Condition tcondition = interactives[tIndex].conditions[j];
                if (tcondition.currentItem.Trim() != "" || tcondition.stateName.Trim() != "")
                {
                    allblank = false;
                }
            }
            if (allblank)
            {
                playThisAction = true;//all condition passed,really can play
            }


            if (playThisAction)
            {
                //check if item is cusuable
                if (isUsingItem)
                {
                    if (GameData.Instance.currentItem!=null && GameData.Instance.currentItem.Trim() !="")
                    {
                        bool isConsumable = GameData.Instance.isConsumable(GameData.Instance.currentItem);
                        if (isConsumable)
                        {
                            GameData.Instance.removeItemByName(GameData.Instance.currentItem);
                            GameData.Instance.SaveGame();
                            GameData.Instance.currentItem = null;
                            GameData.Instance.gameUI.initView();
                        }
                    }
                }
                for (int k = 0; k < interactives[tIndex].playSuccessActions.Count; k++)
                {

                    int tparam = interactives[tIndex].playSuccessActions[k].actionIndex;
                    GameObject tTarget = interactives[tIndex].playSuccessActions[k].actionTarget;
                    tTarget.GetComponent<Actions>().playActionNow(tparam);
                }

            }
            else //not fit the condition,play fail action
            {
                for (int k = 0; k < interactives[tIndex].playFailActions.Count; k++)
                {
                    int tparam = interactives[tIndex].playFailActions[k].actionIndex;
                    GameObject tTarget = interactives[tIndex].playFailActions[k].actionTarget;
                    tTarget.GetComponent<Actions>().playActionNow(tparam);
                }
            }

            return playThisAction;
        }


       

    }


    [System.Serializable]
    public class Interactive
    {
        public bool autoCheck;
        public interactiveTypes myType;
        public List<carryItem> carryItem;
        public List<Condition> conditions;
        public List<playSuccessAction> playSuccessActions;
        public List<playFailAction> playFailActions;
        public List<ConditionComment> conditionComments;
    }




    [System.Serializable]
    //check values
    public class Condition
    {
        public itemOP usingItem;
        public string currentItem;
        public string stateName;
        public operators op;
        public int stateValue;
        public string comment;

    }

    [System.Serializable]
    public class playFailAction
    {
        public string actionName;
        public GameObject actionTarget;
        public bool isSelf;
        public int actionIndex;
    }


    [System.Serializable]
    //play an action index listed in the action compoment of the assigned(through instance id) object
    public class playSuccessAction
    {
        public string actionName;
        public GameObject actionTarget;
        public bool isSelf;
        public int actionIndex;
    }

    [System.Serializable]
    public class ConditionComment
    {
        public string comment;
        
    }


    [System.Serializable]
    public class carryItem
    {

    }


}