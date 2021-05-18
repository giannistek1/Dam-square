using System.Collections;
using System.Collections.Generic;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour
{
    [HideInInspector] public PlaceableObject placeableObject;

    [SerializeField] Image thumbnail;
    public TooltipTrigger tooltipTrigger;

    void Start()
    {
        thumbnail.sprite = placeableObject.thumbnail;
        tooltipTrigger.header = placeableObject.name;
    }

    public void OnClick()
    {
        ObjectPlacer.instance.HandleNewObject(placeableObject.prefab);
        tooltipTrigger.HideTooltip();
    }
}
