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
			    if (Input.mouseScrollDelta.y != 0 || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.F))
			    {
				    tutorial.nextButton.gameObject.SetActive(true);
			    } break;
			    
		    }
		    case 3:	// Zooming
		    {
			    if (GameState.Instance.isRotatingCamera || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
			    {
				    tutorial.nextButton.gameObject.SetActive(true);
			    } break;
		    }
		    case 11:
		    {
				FinishTutorial();
			    break;
		    }
	    }
    }
    
    #endregion

    public void AddTutorialStep()
    {
		// Disable next button at start of steps
		if (tutorialStep >= 0 && tutorialStep <= 3)
		{
			hud.nextButton.gameObject.SetActive(false);
		}
	
		// Start step
		switch (tutorialStep)
		{
			case 0:
			{
				GameState.Instance.isInTutorial = true;
				hud.bottomNavigation.SetActive(false);
				hud.scrollRect.SetActive(false);
				hud.controlsPanel.GetComponent<CanvasGroup>().alpha = 0f;
				hud.controlsPanel.SetActive(false);
				hud.startTutorialGameObject.SetActive(false);
				hud.instructionsButton.SetActive(false);
				hud.settingsButton.SetActive(false);
				hud.deleteAllButton.SetActive(false);
    
				hud.tutorial.gameObject.SetActive(true);
				tutorial.movementTutorial.SetActive(true);
				break;
			}
			case 1:
			{
				hud.movementIndicators.SetActive(false);
				tutorial.movementTutorial.SetActive(false);

				tutorial.zoomImage.SetActive(true);
				break;
			}
			case 2:
			{
				tutorial.zoomImage.SetActive(false);
				tutorial.rotationImage.SetActive(true);
				break;
			}
			case 3:
			{
				tutorial.rotationImage.SetActive(false);
				hud.bottomNavigation.SetActive(true);
				tutorial.clickAllImage.SetActive(true);
				break;
			}
		}
		tutorialStep++;
	}
	
		public void AddAllClickTutorialStep()
		{
			if (tutorialStep != 4) return;
		
			tutorialStep++;
			tutorial.clickAllImage.SetActive(false);
			hud.scrollRect.SetActive(true);
			tutorial.clickObjectSelectorImage.SetActive(true);
			DialogueManager.Instance.DisplayNextSentence();
		}
	
		public void AddObjectSelectorClickTutorialStep()
		{
			if (tutorialStep != 5) return;
		
			tutorialStep++;
			tutorial.clickObjectSelectorImage.SetActive(false);
			DialogueManager.Instance.DisplayNextSentence();
		}

		public void AddObjectRotationTutorialStep()
		{
			if (tutorialStep != 6) return;
		
			tutorialStep++;
			tutorial.clickObjectSelectorImage.SetActive(false);
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

		public void FinishTutorial()
		{
			GameState.Instance.isInTutorial = false;
			hud.movementIndicators.SetActive(true);
			hud.instructionsButton.SetActive(true);
			//TODO: Could be different to be more efficient
			hud.controlsPanel.GetComponent<CanvasGroup>().alpha = 0f;
			hud.settingsButton.SetActive(true);
			
			//TODO: Can be faster if you use your own list
			GameObject[] objects = GameObject.FindGameObjectsWithTag("Placeable");
			if (objects.Length > 0)
				hud.deleteAllButton.SetActive(true);
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
