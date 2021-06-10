using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Localization;
using _Scripts.Object_Placing;
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
        
        if (LocalizationManager.chosenLanguage == Language.English)
            tooltipTrigger.header = placeableObject.englishName;
        else
            tooltipTrigger.header = placeableObject.dutchName;
        
    }

    public void OnClick()
    {
        TutorialManager.Instance.AddObjectSelectorClickTutorialStep();
        ObjectPlacer.Instance.HandleNewObject(placeableObject.prefab);
        tooltipTrigger.HideTooltip();
    }
}
