using System;
using _Scripts.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Scripts.UI
{
	public class HUD : MonoBehaviour
	{
		#region Fields
		public GameObject startTutorialGameObject;
		public TextMeshProUGUI startTutorialText;
		public GameObject controlsPanel;
		
		public Tutorial tutorial;
		public Button nextButton;
		public TextMeshProUGUI nextButtonText;
		public GameObject dialoguePanel;
		public GameObject dialogueText;
		public TextMeshProUGUI feedbackText;
		
		public GameObject instructionsButton;
		public GameObject settingsButton;
		public GameObject deleteAllButton;
		public GameObject bottomNavigation;
		public GameObject scrollRect;
		public GameObject submitDesignButton;
		
		public GameObject movementIndicators;
		
		private DialogueTrigger startTutorialDialogueTrigger;
		#endregion
	
		#region Unity Methods

		private void Awake()
		{

		}

		private void Start()
		{
			startTutorialDialogueTrigger = startTutorialGameObject.GetComponent<DialogueTrigger>();
			startTutorialDialogueTrigger.dialogue.sentences = new string[9];
			startTutorialDialogueTrigger.dialogue.sentences[0] = LocalizationManager.GetTextByKey("TUTORIAL_1");
			startTutorialDialogueTrigger.dialogue.sentences[1] = LocalizationManager.GetTextByKey("TUTORIAL_2");
			startTutorialDialogueTrigger.dialogue.sentences[2] = LocalizationManager.GetTextByKey("TUTORIAL_3");
			startTutorialDialogueTrigger.dialogue.sentences[3] = LocalizationManager.GetTextByKey("TUTORIAL_4");
			startTutorialDialogueTrigger.dialogue.sentences[4] = LocalizationManager.GetTextByKey("TUTORIAL_5");
			startTutorialDialogueTrigger.dialogue.sentences[5] = LocalizationManager.GetTextByKey("TUTORIAL_6");
			startTutorialDialogueTrigger.dialogue.sentences[6] = LocalizationManager.GetTextByKey("TUTORIAL_7");
			startTutorialDialogueTrigger.dialogue.sentences[7] = LocalizationManager.GetTextByKey("TUTORIAL_8");
			startTutorialDialogueTrigger.dialogue.sentences[8] = LocalizationManager.GetTextByKey("TUTORIAL_9");
		}

		private void Update()
		{
			// TODO: Change to coroutine or ideally an event based on whether language has changed 
			startTutorialText.text = LocalizationManager.GetTextByKey("START_TUTORIAL");
		}

		#endregion

		public void PlayButtonClickSound()
		{
			SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
		}
	}
}
