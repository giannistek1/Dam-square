using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(fileName = "NewPlaceableObject", menuName = "NewPlaceableObject", order = 1)]
    public class PlaceableObject : ScriptableObject
    {
        public ObjectCategory category;
        public new string name;
        public Sprite thumbnail;
        public GameObject prefab;
    }
}