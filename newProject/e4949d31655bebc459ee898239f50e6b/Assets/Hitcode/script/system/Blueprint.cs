using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Hitcode_RoomEscape
{
    [System.Serializable]
    public class Blueprint
    {
       public int finalItemID = -1;
        public string finalItemName;

        public List<string> ingredientsName = new List<string>();
        public List<int> ingredients = new List<int>();
        public List<int> amount = new List<int>();
        public Item finalItem;
        public int amountOfFinalItem;
        public float timeToCraft;


        public Blueprint(List<int> ingredients, List<string> ingredientsName, int amountOfFinalItem, List<int> amount, Item item)
        {
            this.ingredients = ingredients;
            this.ingredientsName = ingredientsName;
            this.amount = amount;
            finalItem = item;
        }

        public Blueprint() { }

    }
}