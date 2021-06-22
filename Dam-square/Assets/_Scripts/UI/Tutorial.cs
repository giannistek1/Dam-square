using System;
using System.Collections;
using _Scripts.Game;
using _Scripts.Localization;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
	public class Tutorial : MonoBehaviour
	{
		#region Fields
		public TextMeshProUGUI stopTutorialButtonText;
		public Button nextButton;
		public TextMeshProUGUI nextButtonText;
		
		public GameObject movementTutorial;
		public GameObject rotationImage;
		public GameObject zoomImage;
		public GameObject clickAllImage;
		public GameObject clickObjectSelectorImage;
		#endregion

		#region Unity Methods

		private void Update()
		{
			stopTutorialButtonText.text = LocalizationManager.GetTextByKey("STOP_TUTORIAL");
		}

		#endregion
	}
}
