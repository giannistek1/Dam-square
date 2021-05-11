using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour
{
    [HideInInspector] public PlacableObject placableObject;

    [SerializeField] Image thumbnail;
    public TooltipTrigger tooltipTrigger;

    void Start()
    {
        thumbnail.sprite = placableObject.thumbnail;
        tooltipTrigger.header = placableObject.name;
    }

    public void OnClick()
    {
        ObjectPlacer.instance.HandleNewObject(placableObject.prefab);
        tooltipTrigger.HideTooltip();
    }
}
