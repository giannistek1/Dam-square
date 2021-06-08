using _Scripts.Localization;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
	public class HUD : MonoBehaviour
	{
		#region Fields

		public TextMeshProUGUI startTutorialText;
		#endregion
	
		#region Unity Methods
		private void Update()
		{
			// TODO: Change to coroutine or ideally an event based on whether language has changed 
			startTutorialText.text = LocalizationManager.GetTextByKey("START_TUTORIAL");
		}

		#endregion
	}
}
