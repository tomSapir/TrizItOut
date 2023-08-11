using UnityEngine;
namespace Hitcode_RoomEscape
{
    public abstract class Saveable : MonoBehaviour
    {
        public abstract SaveInfo Save();
        public abstract void Load(SaveInfo saveInfo);
    }
}