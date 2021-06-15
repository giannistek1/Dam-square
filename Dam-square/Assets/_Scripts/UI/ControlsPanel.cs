using System;
using System.Collections;
using _Scripts.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ControlsPanel : MonoBehaviour
    {
        #region Fields
        public HUD hud;
        
        public TextMeshProUGUI instructionsHeaderText;
        public TextMeshProUGUI moveText;
        public TextMeshProUGUI zoomText;
        public TextMeshProUGUI rotateText;

        public TextMeshProUGUI placeSelectObjectText;
        public TextMeshProUGUI cancelPlacementText;
        public TextMeshProUGUI rotateObjectText;
        public TextMeshProUGUI deleteObjectText;

        public TextMeshProUGUI mouseScreenborderText;
        public TextMeshProUGUI holdAndDragText;
        
        private CanvasGroup canvasGroup;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            StartCoroutine(FadeOutCoroutine());
        }

        private void Update()
        {
            // TODO: Change to coroutine or ideally an event based on whether language has changed
            instructionsHeaderText.text = LocalizationManager.GetTextByKey("INSTRUCTIONS");
            moveText.text = LocalizationManager.GetTextByKey("MOVE");
            zoomText.text = LocalizationManager.GetTextByKey("ZOOM");
            rotateText.text = LocalizationManager.GetTextByKey("ROTATE");
            placeSelectObjectText.text = LocalizationManager.GetTextByKey("PLACE_SELECT_OBJECT");
            cancelPlacementText.text = LocalizationManager.GetTextByKey("CANCEL_PLACEMENT");
            rotateObjectText.text = LocalizationManager.GetTextByKey("ROTATE_OBJECT");
            deleteObjectText.text = LocalizationManager.GetTextByKey("DELETE_OBJECT");
            mouseScreenborderText.text = LocalizationManager.GetTextByKey("MOUSE_TO_SCREENBORDER");
            holdAndDragText.text = LocalizationManager.GetTextByKey("HOLD_AND_DRAG");
        }

        #endregion

        IEnumerator FadeOutCoroutine()
        {
            yield return new WaitForSeconds(5f);
            for (float ft = 1f; ft > -0.05f; ft -= 0.05f) 
            {
                canvasGroup.alpha = ft;
                yield return new WaitForSeconds(.05f);
            }
            hud.instructionsButton.SetActive(true);
        }
    }
}
