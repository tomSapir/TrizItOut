using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hitcode_RoomEscape{
    [System.Serializable]
public class ActionManager : ScriptableObject {



        public Dictionary<GameObject, int> actionsIdDic;
	    public int generateId(GameObject g){
            if (actionsIdDic == null) actionsIdDic = new Dictionary<GameObject, int>();
		


			int tId = Random.Range (1, 99999);
            if (actionsIdDic.ContainsKey(g))
            {
                tId = actionsIdDic[g];
                
            }
            else {
                while(actionsIdDic.ContainsValue(tId)){
                    tId = Random.Range(1, 99999);
                }
            }
            actionsIdDic[g] = tId;

			return tId;
	}

	

}
}
