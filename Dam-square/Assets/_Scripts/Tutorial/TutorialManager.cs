using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Game;
using _Scripts.Localization;
using _Scripts.UI;
using UnityEditor;
using UnityEngine;

// TODO: reference tutorial object
public class TutorialManager : MonoBehaviour
{
    #region Singleton
    private static TutorialManager instance;
    public static TutorialManager Instance
    {
        get { return instance; }
    }
    #endregion
    
    #region Fields
    public List<GameObject> dropzones;
    public HUD hud;
    
    private Tutorial tutorial;
    private int tutorialStep = 0;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        #region Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        #endregion
    }

    private void Start()
    {
	    tutorial = hud.tutorial;
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
				    tutorial.nextButtonText.text = LocalizationManager.GetTextByKey("NEXT");
				    tutorial.nextButton.gameObject.SetActive(true);
			    } break;
		    }
		    case 2: // Rotating
		    {
			    if (GameState.Instance.isRotatingCamera || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
			    {
				    tutorial.nextButton.gameObject.SetActive(true);
			    } break;
		    }
		    case 3:	// Zooming
		    {
			    if (Input.mouseScrollDelta.y != 0 || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.F))
			    {
				    tutorial.nextButton.gameObject.SetActive(true);
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
				hud.nextButton.gameObject.SetActive(false);
			}
		
			// Start step
			switch (tutorialStep)
			{
				case 0:
				{
					hud.bottomNavigation.SetActive(false);
					hud.scrollRect.SetActive(false);
					hud.controlsPanel.SetActive(false);
					hud.startTutorialGameObject.SetActive(false);
	    
					hud.tutorial.gameObject.SetActive(true);
					tutorial.rightMovementImage.gameObject.SetActive(true);
					tutorial.leftMovementImage.gameObject.SetActive(true);
					tutorial.upMovementImage.gameObject.SetActive(true);
					tutorial.downMovementImage.gameObject.SetActive(true);
					break;
				}
				case 1:
				{
					//movementImage.gameObject.SetActive(false);
					tutorial.movementImage.gameObject.SetActive(false);
					tutorial.rightMovementImage.gameObject.SetActive(false);
					tutorial.leftMovementImage.gameObject.SetActive(false);
					tutorial.upMovementImage.gameObject.SetActive(false);
					tutorial.downMovementImage.gameObject.SetActive(false);
					
					tutorial.rotationImage.gameObject.SetActive(true);
					break;
				}
				case 2:
				{
					tutorial.rotationImage.gameObject.SetActive(false);
					tutorial.zoomImage.gameObject.SetActive(true);
					break;
				}
				case 3:
				{
					tutorial.zoomImage.gameObject.SetActive(false);
					hud.bottomNavigation.gameObject.SetActive(true);
					tutorial.clickAllImage.gameObject.SetActive(true);
					break;
				}
			}
			tutorialStep++;
		}
	
		public void AddAllClickTutorialStep()
		{
			if (tutorialStep != 4) return;
		
			tutorialStep++;
			tutorial.clickAllImage.gameObject.SetActive(false);
			hud.scrollRect.gameObject.SetActive(true);
			tutorial.clickObjectSelectorImage.gameObject.SetActive(true);
			DialogueManager.Instance.DisplayNextSentence();
		}
	
		public void AddObjectSelectorClickTutorialStep()
		{
			if (tutorialStep != 5) return;
		
			tutorialStep++;
			tutorial.clickObjectSelectorImage.gameObject.SetActive(false);
			DialogueManager.Instance.DisplayNextSentence();
		}

		public void AddObjectRotationTutorialStep()
		{
			if (tutorialStep != 6) return;
		
			tutorialStep++;
			tutorial.clickObjectSelectorImage.gameObject.SetActive(false);
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
			tutorial.nextButton.gameObject.SetActive(true);
			tutorial.nextButtonText.text = LocalizationManager.GetTextByKey("FINISH");
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
