using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Hitcode_RoomEscape
{
    //[System.Serializable]
    public class StatesDataBase : ScriptableObject
    {             //The scriptableObject where the State getting stored which you create(StateDatabase)

        [SerializeField]
        public List<State> StateList = new List<State>();              //List of it
        public State getStateByID(int id)
        {
            
            for (int i = 0; i < StateList.Count; i++)
            {
                if (StateList[i].StateID == id)
                    return StateList[i].getCopy();
            }
            return null;
        }

        public State getStateByName(string name)
        {
            for (int i = 0; i < StateList.Count; i++)
            {
                if (StateList[i].StateName.ToLower().Equals(name.ToLower()))
                    return StateList[i].getCopy();
            }
            return null;
        }




        
    }
}
