using UnityEngine;

[CreateAssetMenu(fileName = "NewPlacableObject", menuName = "NewPlacableObject", order = 1)]
public class PlacableObject : ScriptableObject
{
    public ObjectCategory category;
    public new string name;
    public Sprite thumbnail;
    public GameObject prefab;
}