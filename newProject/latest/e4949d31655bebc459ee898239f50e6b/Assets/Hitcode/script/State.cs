using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Hitcode_RoomEscape
{
    [System.Serializable]
    public class State
    {
        public string StateName;                                     //StateName of the State
        public int StateID;
        public string stateDescription;
        public int StateValue;
        //StateID of the State
        //public string StateDesc;                                     //StateDesc of the State
        //public Sprite StateIcon;                                     //StateIcon of the State
        //public GameObject StateModel;                                //StateModel of the State
        //public int StateValue = 1;                                   //StateValue is at start 1
        //public StateType StateType;                                   //StateType of the State
        //public float StateWeight;                                    //StateWeight of the State
        //public int maxStack = 1;
        //public int indexStateInList = 999;
        //public int rarity;

        //[SerializeField]
        //public List<StateAttribute> StateAttributes = new List<StateAttribute>();

        public State() { }

        //public State(string name, int id, string desc, Sprite icon, GameObject model, int maxStack, StateType type, string sendmessagetext, List<StateAttribute> StateAttributes)                 //function to create a instance of the State
        //{
        //    StateName = name;
        //    StateID = id;
            //StateDesc = desc;
            //StateIcon = icon;
            //StateModel = model;
            //StateType = type;
            //this.maxStack = maxStack;
            //this.StateAttributes = StateAttributes;
        //}

        public State getCopy()
        {
            return (State)this.MemberwiseClone();
        }


    }
}


