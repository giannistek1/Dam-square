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
            NLDict.Add("TUTORIAL_2", "Gebruik muiswiel om in/uit te zoomen.");
            NLDict.Add("TUTORIAL_3", "Houd rechtermuisknop ingedrukt en sleep om te draaien.");
            NLDict.Add("TUTORIAL_4", "Klik 'ALL' onderaan het scherm.");
            NLDict.Add("TUTORIAL_5", "Klik het eerste object boven 'ALL'.");
            NLDict.Add("TUTORIAL_6", "Gebruik 'Z' of 'X' om object te draaien.");
            NLDict.Add("TUTORIAL_7", "Plaats object in een groen gebied (dropzone)");
            NLDict.Add("TUTORIAL_8", "Klik op geplaatste object om te selecteren.");
            NLDict.Add("TUTORIAL_9", "Druk op 'Delete' of 'Backspace' om geselecteerde object te verwijderen.");
            NLDict.Add("NEXT", "Volgende");
            NLDict.Add("FINISH", "Afronden");
        
            // Instructions
            NLDict.Add("INSTRUCTIONS", "Instructies");
            NLDict.Add("MOVE", "Bewegen");
            NLDict.Add("ZOOM", "Inzoomen/uitzoomen");
            NLDict.Add("ROTATE", "Draaien");
            NLDict.Add("PLACE_SELECT_OBJECT", "Plaats/selecteer object");
            NLDict.Add("CANCEL_PLACEMENT", "Annuleer plaatsen");
            NLDict.Add("ROTATE_OBJECT", "Draai object");
            NLDict.Add("DELETE_OBJECT", "Verwijder object");
            NLDict.Add("MOUSE_TO_SCREENBORDER", "Muis naar schermrand");
            NLDict.Add("HOLD_AND_DRAG", "Ingedrukt + slepen");
            
            // Feedback
            NLDict.Add("OBJECT_PLACED", "Object geplaatst");
            NLDict.Add("OBJECT_DELETED", "Geselecteerde object verwijderd");
            NLDict.Add("CANT_PLACE_HERE", "Kan object niet hier plaatsen. Plaats in een geldig gebied");
            NLDict.Add("DELETED_ALL_OBJECTS", "Alle objecten succesvol verwijderd");
            NLDict.Add("CANT_PLACE_HERE_DROPZONE", "Kan object niet hier plaatsen. Plaats object in een groen vlak!");
            NLDict.Add("CANT_PLACE_HERE_OBJECT", "Object staat in de weg.");
            NLDict.Add("GOOD_JOB", "Goed gedaan!");

            NLDict.Add("SUBMIT_DESIGN", "Verstuur ontwerp");
        
            NLDict.Add("PLAY", "Start");
            NLDict.Add("SETTINGS", "Instellingen");
            NLDict.Add("OPTIONS", "Opties");
            NLDict.Add("LANGUAGE", "Taal");
            NLDict.Add("DUTCH", "Nederlands");
            NLDict.Add("ENGLISH_US", "Engels (Amerikaans)");
            NLDict.Add("ENGLISH_UK", "Engels (Brits)");
            NLDict.Add("MUSIC", "Muziek");
            NLDict.Add("SOUND", "Geluid");
            NLDict.Add("RETURN", "Terug");
            NLDict.Add("ON", "AAN");
            NLDict.Add("OFF", "UIT");
            NLDict.Add("DELETE_ALL_POPUP", "Weet u zeker dat u alle geplaatste objecten wilt verwijderen?");
            NLDict.Add("YES", "Ja");
            NLDict.Add("NO", "Nee");
            NLDict.Add("LOADING", "Laden...");
            NLDict.Add("LOGOUT", "Log uit");
            NLDict.Add("CLOSE", "Sluiten");
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
            ENDict.Add("TUTORIAL_2", "Use scrollwheel to zoom in/out.");
            ENDict.Add("TUTORIAL_3", "Hold right mouse button and drag to change viewing angle.");
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
            ENDict.Add("MOVE", "Move");
            ENDict.Add("ZOOM", "Zoom in/out");
            ENDict.Add("ROTATE", "Rotate");
            ENDict.Add("PLACE_SELECT_OBJECT", "Place/select object");
            ENDict.Add("CANCEL_PLACEMENT", "Cancel placement");
            ENDict.Add("ROTATE_OBJECT", "Rotate object");
            ENDict.Add("DELETE_OBJECT", "Delete object");
            ENDict.Add("MOUSE_TO_SCREENBORDER", "Mouse to screenborder");
            ENDict.Add("HOLD_AND_DRAG", "Hold + drag");

            // Feedback
            ENDict.Add("OBJECT_PLACED", "Object placed");
            ENDict.Add("OBJECT_DELETED", "Succesfully deleted selected object");
            ENDict.Add("DELETED_ALL_OBJECTS", "Succesfully deleted all placed objects");
            ENDict.Add("CANT_PLACE_HERE", "Can't place object here. Place object in valid area");
            ENDict.Add("CANT_PLACE_HERE_DROPZONE", "Can't place object here. Place object in green area.");
            ENDict.Add("CANT_PLACE_HERE_OBJECT", "Object is in the way!");
            ENDict.Add("GOOD_JOB", "Good job!");
            
            ENDict.Add("SUBMIT_DESIGN", "Submit design");
        
            // Menutext
            ENDict.Add("PLAY", "Play");
            ENDict.Add("SETTINGS", "Settings");
            ENDict.Add("OPTIONS", "Options");
            ENDict.Add("LANGUAGE", "Language");
            ENDict.Add("DUTCH", "Dutch");
            ENDict.Add("ENGLISH_US", "English (US)");
            ENDict.Add("ENGLISH_UK", "English (UK)");
            ENDict.Add("MUSIC", "Music");
            ENDict.Add("SOUND", "Sound");
            ENDict.Add("ON", "ON");
            ENDict.Add("OFF", "OFF");
            ENDict.Add("DELETE_ALL_POPUP", "Are you sure you want to delete all placed objects?");
            ENDict.Add("YES", "Yes");
            ENDict.Add("NO", "No");
            ENDict.Add("RETURN", "Return");
            ENDict.Add("LOADING", "Loading...");
            ENDict.Add("LOGOUT", "Logout");
            ENDict.Add("CLOSE", "Close");
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
