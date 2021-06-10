using System.Collections;
using _Scripts.Game;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
	public class Tutorial : MonoBehaviour
	{
		#region Fields
		public Button nextButton;
		public TextMeshProUGUI nextButtonText;

		public Image movementImage;
		public Image rightMovementImage;
		public Image leftMovementImage;
		public Image upMovementImage;
		public Image downMovementImage;
		public Image rotationImage;
		public Image zoomImage;
		public Image clickAllImage;
		public Image clickObjectSelectorImage;

		private int tutorialStep = 0;
		#endregion
    
		#region Singleton
		private static Tutorial instance;
		public static Tutorial Instance { get { return instance; } }
		#endregion

		#region Unity Methods
		private void Awake()
		{
			#region Singleton
			if (instance != null && instance != this)
			{
				Destroy(this.gameObject);
			} else {
				instance = this;
			}
			#endregion
		}

		private void Update()
		{
			// Finished step
			switch (tutorialStep)
			{
				case 1: // Moving
				{
					if (GameState.Instance.playerIsMoving || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) 
					    || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
					{
						nextButton.gameObject.SetActive(true);
					} break;
				}
				case 2: // Rotating
				{
					if (GameState.Instance.isRotatingCamera || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
					{
						nextButton.gameObject.SetActive(true);
					} break;
				}
				case 3:	// Zooming
				{
					if (Input.mouseScrollDelta.y != 0 || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.F))
					{
						nextButton.gameObject.SetActive(true);
					} break;
				}
			}
		}
		#endregion
	}
}
