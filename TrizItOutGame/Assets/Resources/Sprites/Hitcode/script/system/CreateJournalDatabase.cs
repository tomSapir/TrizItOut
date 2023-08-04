using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hitcode_RoomEscape
{
    [System.Serializable]
    public class CreateJournalDatabase
    {
        [SerializeField]
        public static JournalDataBase asset;                                                  

#if UNITY_EDITOR
        public static JournalDataBase createJounalDatabase(string projectName = "")                                    
        {
            asset = ScriptableObject.CreateInstance<JournalDataBase>();                    
           
            AssetDatabase.CreateAsset(asset, "Assets/Hitcode/src/Resources/" + projectName + "/JournalDataBase.asset");    
            AssetDatabase.SaveAssets();                                                     
            return asset;
        }
#endif

    }

}