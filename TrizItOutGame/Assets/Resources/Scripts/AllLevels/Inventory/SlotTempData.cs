using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotTempData
{
    public Sprite SlotImageSprite { get; set; }
    public string CombinationItem { get; set; }
    public SlotManager.Property Property { get; set; }
    public int AmountOfUsage { get; set; }
    public string DisplayImageName { get; set; }

    public SlotTempData(Sprite i_SlotImageSprite, string i_CombinationItem, 
        SlotManager.Property i_Property, int i_AmountOfUsage, string i_DisplayImageName)
    {
        SlotImageSprite = i_SlotImageSprite;
        CombinationItem = i_CombinationItem;
        Property = i_Property;
        AmountOfUsage = i_AmountOfUsage;
        DisplayImageName = i_DisplayImageName;
    }
}
