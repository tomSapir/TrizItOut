using UnityEngine;
using System;
using System.Collections;

namespace Hitcode_RoomEscape.UI
{
  
    public class ListScrollerCellView : MonoBehaviour
    {
     
        public string cellIdentifier;

     
        [NonSerialized]
        public int cellIndex;

      
        [NonSerialized]
        public int dataIndex;

      
        [NonSerialized]
        public bool active;

        public virtual void RefreshCellView() { }
    }
}