﻿using UnityEngine;
using System.Collections;
namespace Hitcode_RoomEscape
{
    [System.Serializable]
    public class ItemAttribute
    {

        public string attributeName;
        public int attributeValue;
        public ItemAttribute(string attributeName, int attributeValue)
        {
            this.attributeName = attributeName;
            this.attributeValue = attributeValue;
        }

        public ItemAttribute() { }

    }

}