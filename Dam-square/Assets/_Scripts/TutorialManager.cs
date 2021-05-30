using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    #region Fields

    public Button nextButton;
    public TextMeshProUGUI nextButtonText;

    public GameObject[] dropzones;
    
    public Image movementImage;
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
			case 0: // Moving
			{
				if (GameManager.Instance.isDraggingCamera || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) 
				    || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
				{
					nextButton.gameObject.SetActive(true);
				} break;
			}
			case 1: // Rotating
			{
				if (GameManager.Instance.isRotatingCamera || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
				{
					nextButton.gameObject.SetActive(true);
				} break;
			}
			case 2:	// Zooming
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
		tutorialStep++;
		
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
				movementImage.gameObject.SetActive(true);
				break;
			}
			case 1:
			{
				movementImage.gameObject.SetActive(false);
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
				clickAllImage.gameObject.SetActive(true);
				break;
			}
		}
	}
	
	public void AddAllClickTutorialStep()
	{
		if (tutorialStep != 3) return;
		
		tutorialStep++;
		clickAllImage.gameObject.SetActive(false);
		clickObjectSelectorImage.gameObject.SetActive(true);
		DialogueManager.Instance.DisplayNextSentence();
	}
	
	public void AddObjectSelectorClickTutorialStep()
	{
		if (tutorialStep != 4) return;
		
		tutorialStep++;
		clickObjectSelectorImage.gameObject.SetActive(false);
		DialogueManager.Instance.DisplayNextSentence();
	}

	public void AddObjectRotationTutorialStep()
	{
		if (tutorialStep != 5) return;
		
		tutorialStep++;
		clickObjectSelectorImage.gameObject.SetActive(false);
		MakeDropzonesBlink();
		DialogueManager.Instance.DisplayNextSentence();

	}
	
	public void AddDropzoneTutorialStep()
	{
		if (tutorialStep != 6) return;
		
		tutorialStep++;
		DialogueManager.Instance.DisplayNextSentence();
	}
	
	public void AddObjectSelectionTutorialStep()
	{
		if (tutorialStep != 7) return;
		
		tutorialStep++;
		DialogueManager.Instance.DisplayNextSentence();
	}
	
	public void AddObjectDeletionTutorialStep()
	{
		if (tutorialStep != 8) return;
		
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
