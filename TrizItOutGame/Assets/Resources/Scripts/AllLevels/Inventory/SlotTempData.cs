using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTempData
{
    public string ItemName { get; set; } 
    public Sprite SlotImageSprite { get; set; } 
    public string CombinationItem { get; set; } 
    public SlotManager.eProperty Property { get; set; } 
    public int AmountOfUsage { get; set; } 
    public string DisplayImageName { get; set; }

    public SlotTempData(string i_ItemName, Sprite i_SlotImageSprite, string i_CombinationItem, 
        SlotManager.eProperty i_Property, int i_AmountOfUsage, string i_DisplayImageName)
    {
        ItemName = i_ItemName;
        SlotImageSprite = i_SlotImageSprite;
        CombinationItem = i_CombinationItem;
        Property = i_Property;
        AmountOfUsage = i_AmountOfUsage;
        DisplayImageName = i_DisplayImageName;
    }
}
