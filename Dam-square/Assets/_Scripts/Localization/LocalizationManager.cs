using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        // PreferredLanguage is the language being used.
        // KEYS are CASE-SENSITIVE
        public static Language chosenLanguage;
        private static Dictionary<string, string> NLDict;
        private static Dictionary<string, string> ENDict;
    
        #region Singleton
        private static LocalizationManager _instance;
        public static LocalizationManager Instance { get { return _instance; } }
        #endregion

        protected void Awake()
        {
            #region Singleton
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
            #endregion
        
            DontDestroyOnLoad(this); // Persistent

            chosenLanguage = ClientConfigurationManager.Instance.clientConfiguration.preferredLanguage; // This is by default set in ClientConfigurationManager
            NLDict = new Dictionary<string, string>();
            ENDict = new Dictionary<string, string>();
            
            UpdateDutchDictionaryStatic();
            UpdateEnglishDictionaryStatic();
        }
        private static void UpdateDutchDictionaryStatic()
        {
            // Account
            NLDict.Add("NAME", "Naam");
            NLDict.Add("EMAIL", "E-mail");
            NLDict.Add("LOGIN", "Log in");
            NLDict.Add("LOGGED_IN_AS", "Ingelogd als");
            NLDict.Add("PASSWORD", "Wachtwoord");
        
            // HUD
            NLDict.Add("START_TUTORIAL", "Start oefenscenario");
        
            // Instructions
            NLDict.Add("INSTRUCTIONS", "Instructies");
            NLDict.Add("INSTRUCTIONS_1", "Bewegen \nInzoomen/uitzoomen \nDraaien \n\nPlaats object \nAnnuleer objectplaatsing \nPak object op \nObject rechts draaien \nObject links draaien \n Object verwijderen");
            NLDict.Add("INSTRUCTIONS_2", "Muis op schermrand \nScrollwiel \nRMK + slepen \n\nLMK \nEsc \nLMK \nZ \nX \nDelete / Backspace");
            NLDict.Add("MOVE", "Bewegen");
            NLDict.Add("ROTATE", "Draaien");
            NLDict.Add("ZOOM_IN_OUT", "Inzoomen/uitzoomen");
            NLDict.Add("ROTATE_OBJECT", "Draai object");
            NLDict.Add("DELETE_OBJECT", "Verwijder object");
        
            NLDict.Add("SUBMIT_DESIGN", "Verstuur ontwerp");
        
            NLDict.Add("PLAY", "Start");
            NLDict.Add("LEVEL", "Level");
            NLDict.Add("OPTIONS", "Opties");
            NLDict.Add("MUSIC", "Muziek");
            NLDict.Add("SOUND", "Geluid");
            NLDict.Add("RETURN", "Terug");
            NLDict.Add("ON", "AAN");
            NLDict.Add("OFF", "UIT");
            NLDict.Add("LOADING", "Laden...");
            NLDict.Add("LOGOUT", "Log uit");
            NLDict.Add("VERSION", "Versie");
            NLDict.Add("COMPLETED", "Behaald");
            NLDict.Add("NOT_COMPLETED", "Niet behaald");
        }

        private static void UpdateEnglishDictionaryStatic()
        {
            // Account
            ENDict.Add("NAME", "Name");
            ENDict.Add("EMAIL", "Email");
            ENDict.Add("PASSWORD", "Password");
            ENDict.Add("LOGIN", "Login");
            ENDict.Add("LOGGED_IN_AS", "Logged in as");
        
            // HUD
            ENDict.Add("START_TUTORIAL", "Start tutorial");
        
            // Instructions
            ENDict.Add("INSTRUCTIONS", "Instructions");
            ENDict.Add("INSTRUCTIONS_1", "Move \nZoom in/out \nRotate \n\nPlace object \nCancel objectplacement \nPick up object \nSpin object right \nSpin object left\n Delete object");
            ENDict.Add("INSTRUCTIONS_2", "Mouse to screenborder \nScrollwheel \nHold RMB + drag \n\nPress LMB \nEsc \nPress LMB \nZ \nX \nDelete / Backspace");
            ENDict.Add("MOVE", "Move");
            ENDict.Add("ROTATE", "Rotate");
            ENDict.Add("ZOOM_IN_OUT", "Zoom in/out");
            ENDict.Add("ROTATE_OBJECT", "Rotate object");
            ENDict.Add("DELETE_OBJECT", "Delete object");
        
            ENDict.Add("SUBMIT_DESIGN", "Submit design");
        
            // Menutext
            ENDict.Add("PLAY", "Play");
            ENDict.Add("LEVEL", "Level");
            ENDict.Add("OPTIONS", "Options");
            ENDict.Add("MUSIC", "Music");
            ENDict.Add("SOUND", "Sound");
            ENDict.Add("ON", "ON");
            ENDict.Add("OFF", "OFF");
            ENDict.Add("RETURN", "Return");
            ENDict.Add("LOADING", "Loading...");
            ENDict.Add("LOGOUT", "Logout");
            ENDict.Add("VERSION", "Version");
            ENDict.Add("COMPLETED", "Completed");
            ENDict.Add("NOT_COMPLETED", "Not completed");
        }

        #region GetTextByKey

        public static string GetTextByKey(string key)
        {
            try
            {
                switch (chosenLanguage)
                {
                    case Language.Dutch:
                        if (NLDict.ContainsKey(key))
                            return NLDict[key];
                        else
                            return key;
                    default:
                    case Language.English:
                        if (ENDict.ContainsKey(key))
                            return ENDict[key];
                        else
                            return key;
                }
            }
            catch (Exception ex)
            {
                print("KEY NOT FOUND IN A DICT: " + key);
                NLDict = new Dictionary<string, string>();
                ENDict = new Dictionary<string, string>();
                UpdateDutchDictionaryStatic();
                UpdateEnglishDictionaryStatic();
            }

            return key;
        }

        #endregion

        public void SetLanguage(int languageIndex)
        {
            chosenLanguage = (Language)languageIndex;
            // Change preference
            ClientConfigurationManager.Instance.clientConfiguration.preferredLanguage = (Language)languageIndex;
            ClientConfigurationManager.Instance.SavePlayerSettings();
        }
    }
}
