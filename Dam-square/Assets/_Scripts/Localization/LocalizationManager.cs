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
            
            // Tutorial
            NLDict.Add("TUTORIAL_1", "Beweeg muis naar schermrand om te bewegen.");
            NLDict.Add("TUTORIAL_2", "Houd rechtermuisknop ingedrukt en slepen om perspectief te draaien.");
            NLDict.Add("TUTORIAL_3", "Gebruik muiswiel om in/uit te zoomen.");
            NLDict.Add("TUTORIAL_4", "Klik 'ALL' onderaan het scherm.");
            NLDict.Add("TUTORIAL_5", "Klik het eerst object.");
            NLDict.Add("TUTORIAL_6", "Gebruik 'Z' of 'X' om object te draaien.");
            NLDict.Add("TUTORIAL_7", "Plaats object in een groen (dropzone)");
            NLDict.Add("TUTORIAL_8", "Klik op geplaatste object om te selecteren.");
            NLDict.Add("TUTORIAL_9", "Druk op 'Delete' of 'Backspace' om geselecteerde object te verwijderen.");
            NLDict.Add("NEXT", "Volgende");
            NLDict.Add("FINISH", "Afronden");
        
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
            
            // Tutorial
            ENDict.Add("TUTORIAL_1", "Move mouse to border of screen.");
            ENDict.Add("TUTORIAL_2", "Hold right mouse button and drag to change viewing angle.");
            ENDict.Add("TUTORIAL_3", "Use scrollwheel to zoom in/out.");
            ENDict.Add("TUTORIAL_4", "Click 'ALL' at bottom of screen.");
            ENDict.Add("TUTORIAL_5", "Click the first object.");
            ENDict.Add("TUTORIAL_6", "Use 'Z' or 'X' to rotate object.");
            ENDict.Add("TUTORIAL_7", "Place object in green area (dropzone).");
            ENDict.Add("TUTORIAL_8", "Click placed object to select it.");
            ENDict.Add("TUTORIAL_9", "Press 'delete' or 'backspace' to delete selected object.");
            ENDict.Add("NEXT", "Next");
            ENDict.Add("FINISH", "Finish");
        
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
