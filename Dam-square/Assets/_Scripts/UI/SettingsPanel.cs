using System;
using _Scripts;
using _Scripts.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    #region Fields
    public TextMeshProUGUI settingsHeaderText;
    public TextMeshProUGUI languageText;
    public TextMeshProUGUI dutchText;
    public TextMeshProUGUI englishUSText;

    public Toggle nlToggle;
    public Toggle usToggle;
	#endregion
	
	#region Unity Methods

	private void Start()
	{
		if (ClientConfigurationManager.Instance.clientConfiguration.preferredLanguage == Language.Dutch)
			nlToggle.isOn = true;
		else
			usToggle.isOn = true;
	}

	private void Update()
	{
		// TODO: Change to coroutine or ideally an event based on whether language has changed 
		settingsHeaderText.text = LocalizationManager.GetTextByKey("SETTINGS");
		languageText.text = LocalizationManager.GetTextByKey("LANGUAGE");
		dutchText.text = LocalizationManager.GetTextByKey("DUTCH");
		englishUSText.text = LocalizationManager.GetTextByKey("ENGLISH_US");
	}

	#endregion
}
