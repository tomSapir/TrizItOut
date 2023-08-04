using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hitcode_RoomEscape
{
    public class CreateItemDatabase
    {
        public static ItemsDataBase asset;                                                  //The List of all Items

#if UNITY_EDITOR
        public static ItemsDataBase createItemDatabase(string projectName = "")                                    //creates a new ItemDatabase(new instance)
        {
            asset = ScriptableObject.CreateInstance<ItemsDataBase>();                       //of the ScriptableObject InventoryItemList

            AssetDatabase.CreateAsset(asset, "Assets/Hitcode/src/Resources/"+projectName+"/ItemDatabase.asset");            //in the Folder Assets/Resources/ItemDatabase.asset
            AssetDatabase.SaveAssets();                                                         //and than saves it there
            //asset.itemList.Add(new Item());
            return asset;
        }
#endif




    }
}
