using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Hitcode_RoomEscape
{

    public class JournalDataBase : ScriptableObject
    {            

        [SerializeField]
        public List<JournalData> itemList = new List<JournalData>();              //List of it

     
    }
}
