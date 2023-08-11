using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hitcode_RoomEscape
{
    [System.Serializable]
    public class CreateStateDatabase
    {
        [SerializeField]
        public static StatesDataBase asset;                                                  //The List of all Items

#if UNITY_EDITOR
        public static StatesDataBase createStateDatabase(string projectName = "")                                    //creates a new ItemDatabase(new instance)
        {
            asset = ScriptableObject.CreateInstance<StatesDataBase>();                       //of the ScriptableObject InventoryItemList
           
            AssetDatabase.CreateAsset(asset, "Assets/Hitcode/src/Resources/" + projectName + "/StateDataBase.asset");      //in the Folder Assets/Resources/ItemDatabase.asset
            AssetDatabase.SaveAssets();                                                         //and than saves it there
            return asset;
        }
#endif

    }

}