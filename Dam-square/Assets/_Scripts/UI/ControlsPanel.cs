using _Scripts.Localization;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class ControlsPanel : MonoBehaviour
    {
        #region Fields
        public TextMeshProUGUI instructionsHeaderText;
        public TextMeshProUGUI commandsText;
        public TextMeshProUGUI controlsText;
        #endregion

        #region Unity Methods

        private void Update()
        {
            // TODO: Change to coroutine or ideally an event based on whether language has changed
            instructionsHeaderText.text = LocalizationManager.GetTextByKey("INSTRUCTIONS");
            commandsText.text = LocalizationManager.GetTextByKey("INSTRUCTIONS_1");
            controlsText.text = LocalizationManager.GetTextByKey("INSTRUCTIONS_2");
        }

        #endregion
    }
}
