using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Hitcode_RoomEscape
{
    public class BlueprintDatabase : ScriptableObject
    {
        [SerializeField]
        public List<Blueprint> blueprints = new List<Blueprint>();
    }
}
