using System;
using System.Text.RegularExpressions;
using _Scripts;
using _Scripts.Game;
using _Scripts.Localization;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreen : MonoBehaviour
{
    #region Fields
    public HUD hud;
    
    public TextMeshProUGUI welcomeText;
    public TextMeshProUGUI introText;
    public TMP_InputField nameField;
    public TMP_InputField emailField;
    public Toggle rememberMeToggle;
    public TextMeshProUGUI rememberMeText;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI getStartedText;

    private TextMeshProUGUI namePlaceholder;
    private TextMeshProUGUI emailPlaceHolder;
    #endregion

    #region Unity Methods

    private void Start()
    {
        errorText.text = "";
        namePlaceholder = nameField.placeholder.GetComponent<TextMeshProUGUI>();
        emailPlaceHolder = emailField.placeholder.GetComponent<TextMeshProUGUI>();

        nameField.text = ClientConfigurationManager.Instance.clientConfiguration.preferredName;
        emailField.text = ClientConfigurationManager.Instance.clientConfiguration.preferredEmail;
    }

    private void Update()
    {
        // TODO: Text not every update
        welcomeText.text = LocalizationManager.GetTextByKey("WELCOME");
        introText.text = LocalizationManager.GetTextByKey("FILL_IN_NAME_AND_EMAIL")+":";
        namePlaceholder.text = LocalizationManager.GetTextByKey("NAME")+"...";
        emailPlaceHolder.text = LocalizationManager.GetTextByKey("EMAIL")+"..."; 
        rememberMeText.text = LocalizationManager.GetTextByKey("REMEMBER_ME");
        getStartedText.text = LocalizationManager.GetTextByKey("GET_STARTED"); 
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (nameField.isFocused)
            {
                emailField.Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Login();
        }
    }

    #endregion

    public void Login()
    {
        if (nameField.text.Length < 3) { errorText.text = LocalizationManager.GetTextByKey("NAME_HAS_TO_BE_LONGER");
            return;
        }
        if (!IsValidEmailAddress(emailField.text)) { errorText.text = LocalizationManager.GetTextByKey("USE_VALID_EMAIL"); 
            return; 
        }

        if (rememberMeToggle.isOn)
        {
            print("Saving name and email");
            // Save name and email for future use
            ClientConfigurationManager.Instance.clientConfiguration.preferredName = nameField.text;
            ClientConfigurationManager.Instance.clientConfiguration.preferredEmail = emailField.text;
            ClientConfigurationManager.Instance.SavePlayerSettings();
        }
        else
        {
            // Clear settings
            ClientConfigurationManager.Instance.clientConfiguration.preferredName = "";
            ClientConfigurationManager.Instance.clientConfiguration.preferredEmail = "";
            ClientConfigurationManager.Instance.SavePlayerSettings();
        }
        
        // Save name and email in current game
        GameState.Instance.playerName = nameField.text;
        GameState.Instance.playerEmail = emailField.text;

        errorText.text = "";

        GameState.Instance.hasLoggedIn = true;
        
        hud.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    
    public bool IsValidEmailAddress(string s)
    {
        var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        return regex.IsMatch(s);
    }
}
