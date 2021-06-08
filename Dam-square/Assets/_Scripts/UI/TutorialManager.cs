using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
	public class TutorialManager : MonoBehaviour
	{
		#region Fields

		public Button nextButton;
		public TextMeshProUGUI nextButtonText;

		public GameObject[] dropzones;
		public GameObject bottomNavigation;
		public GameObject objectSelectorList;
    
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
		private static TutorialManager instance;
		public static TutorialManager Instance { get { return instance; } }
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
			// Finsihed step
			switch (tutorialStep)
			{
				case 1: // Moving
				{
					if (GameManager.Instance.playerIsMoving || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) 
					    || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
					{
						nextButton.gameObject.SetActive(true);
					} break;
				}
				case 2: // Rotating
				{
					if (GameManager.Instance.isRotatingCamera || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
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

		public void AddTutorialStep()
		{
			// Disable next button
			if (tutorialStep >= 0 && tutorialStep <= 3)
			{
				nextButton.gameObject.SetActive(false);
			}
		
			// Start step
			switch (tutorialStep)
			{
				case 0:
				{
					//movementImage.gameObject.SetActive(true);
					bottomNavigation.gameObject.SetActive(false);
					objectSelectorList.gameObject.SetActive(false);
					rightMovementImage.gameObject.SetActive(true);
					leftMovementImage.gameObject.SetActive(true);
					upMovementImage.gameObject.SetActive(true);
					downMovementImage.gameObject.SetActive(true);
					break;
				}
				case 1:
				{
					//movementImage.gameObject.SetActive(false);
					movementImage.gameObject.SetActive(false);
					rightMovementImage.gameObject.SetActive(false);
					leftMovementImage.gameObject.SetActive(false);
					upMovementImage.gameObject.SetActive(false);
					downMovementImage.gameObject.SetActive(false);
					
					rotationImage.gameObject.SetActive(true);
					break;
				}
				case 2:
				{
					rotationImage.gameObject.SetActive(false);
					zoomImage.gameObject.SetActive(true);
					break;
				}
				case 3:
				{
					zoomImage.gameObject.SetActive(false);
					bottomNavigation.gameObject.SetActive(true);
					clickAllImage.gameObject.SetActive(true);
					break;
				}
			}
			tutorialStep++;
		}
	
		public void AddAllClickTutorialStep()
		{
			if (tutorialStep != 4) return;
		
			tutorialStep++;
			clickAllImage.gameObject.SetActive(false);
			clickObjectSelectorImage.gameObject.SetActive(true);
			DialogueManager.Instance.DisplayNextSentence();
		}
	
		public void AddObjectSelectorClickTutorialStep()
		{
			if (tutorialStep != 5) return;
		
			tutorialStep++;
			clickObjectSelectorImage.gameObject.SetActive(false);
			DialogueManager.Instance.DisplayNextSentence();
		}

		public void AddObjectRotationTutorialStep()
		{
			if (tutorialStep != 6) return;
		
			tutorialStep++;
			clickObjectSelectorImage.gameObject.SetActive(false);
			MakeDropzonesBlink();
			DialogueManager.Instance.DisplayNextSentence();

		}
	
		public void AddDropzoneTutorialStep()
		{
			if (tutorialStep != 7) return;
		
			tutorialStep++;
			DialogueManager.Instance.DisplayNextSentence();
		}
	
		public void AddObjectSelectionTutorialStep()
		{
			if (tutorialStep != 8) return;
		
			tutorialStep++;
			DialogueManager.Instance.DisplayNextSentence();
		}
	
		public void AddObjectDeletionTutorialStep()
		{
			if (tutorialStep != 9) return;
		
			tutorialStep++;
			DialogueManager.Instance.DisplayNextSentence();
			nextButton.gameObject.SetActive(true);
			nextButtonText.text = "Afronden";
		}

		public void MakeDropzonesBlink()
		{
			foreach (GameObject dropzone in dropzones)
			{
				StartCoroutine(Blink(3f, dropzone));
			}
		}
	
		IEnumerator Blink(float waitTime, GameObject dropzone)
		{
			Renderer renderer = dropzone.GetComponent<Renderer>();
			var endTime=Time.time + waitTime;
			while (Time.time < endTime)
			{
				renderer.enabled = false;
				yield return new WaitForSeconds(0.2f);
				renderer.enabled = true;
				yield return new WaitForSeconds(0.5f);
			}
		}
	}
}
