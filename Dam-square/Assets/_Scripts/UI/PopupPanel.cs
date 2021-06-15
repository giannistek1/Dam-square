using System;
using _Scripts.Localization;
using TMPro;
using UnityEngine;

public class PopupPanel : MonoBehaviour
{
    #region Fields
    public TextMeshProUGUI popupDescriptionText;
    public TextMeshProUGUI yesText;
    public TextMeshProUGUI noText;
    #endregion

    #region Unity Methods

    private void Update()
    {
        //TODO Translate when triggered
        popupDescriptionText.text = LocalizationManager.GetTextByKey("DELETE_ALL_POPUP");
        yesText.text = LocalizationManager.GetTextByKey("YES");
        noText.text = LocalizationManager.GetTextByKey("NO");
    }

    #endregion
}
