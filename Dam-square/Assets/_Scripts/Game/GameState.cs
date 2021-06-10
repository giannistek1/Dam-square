using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts;
using _Scripts.Localization;
using _Scripts.Object_Placing;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Game
{
	[Serializable]
	public struct GameManagers
	{
		//public ClientConfigurationManager clientConfigurationManagerClass;
		public GUIManager GUIManagerClass;
		//public LocalizationManager localizationManagerClass;
		public ObjectPlacer objectPlacerClass;
		//public DialogueManager dialogueManagerClass;
		//public TutorialManager tutorialManagerClass;
	}

	public class GameState : MonoBehaviour
	{
		#region Fields
		public GameObject mainCamera;
		public HUD hud;
		public bool playerIsMoving = false;
		public bool isRotatingCamera = false;

		public List<GameObject> dropzones;
		
		[SerializeField]
		private GameManagers gameManagers;

		private Vector3 startPos;
		private Vector3 lastPos;
		#endregion
	
		#region Singleton
		private static GameState instance;
		public static GameState Instance { get { return instance; } }
		#endregion
	
		#region Unity Methods

		private void Awake()
		{
#if UNITY_EDITOR 
			Debug.unityLogger.logEnabled = true;
#else 
			Debug.unityLogger.logEnabled = false;
#endif
		
			#region Singleton
			if (instance != null && instance != this)
			{
				Destroy(this.gameObject);
			} else {
				instance = this;
			}
			#endregion
		
			// Asserts
			//Assert.IsNotNull(gameManagers.clientConfigurationManagerClass);
			Assert.IsNotNull(gameManagers.GUIManagerClass);
			//Assert.IsNotNull(gameManagers.localizationManagerClass);
			Assert.IsNotNull(gameManagers.objectPlacerClass);
			//Assert.IsNotNull(gameManagers.dialogueManagerClass);
			//Assert.IsNotNull(gameManagers.tutorialManagerClass);
			//Assert.IsNotNull(backgroundSoundManager);
		}

		private void Start()
		{
			//ClientConfigurationManager clientConfigurationManager = Instantiate(gameManagers.clientConfigurationManagerClass);
			GUIManager guiManager = Instantiate(gameManagers.GUIManagerClass);
			guiManager.hud = hud;
			//LocalizationManager localizationManager = Instantiate(gameManagers.localizationManagerClass);
			ObjectPlacer objectPlacer = Instantiate(gameManagers.objectPlacerClass);
			objectPlacer.hud = hud;
			//DialogueManager dialogueManager = Instantiate(gameManagers.dialogueManagerClass);
			//dialogueManager.hud = hud;
			//TutorialManager tutorialManager = Instantiate(gameManagers.tutorialManagerClass);
			//tutorialManager.hud = hud;

			//tutorialManager.dropzones = new List<GameObject>();
			//tutorialManager.dropzones.Add(dropzones.Last());

			lastPos = mainCamera.transform.position;
		}

		void Update()
		{
			// Check for drag action
			//CheckDraggingAction();
			CheckCharMoved();

			CheckRotatingAction();
		}
		#endregion

		/*private void CheckDraggingAction()
	{
		if (Input.GetMouseButtonDown(0))
		{
			startPos = Input.mousePosition;
		} 
		if (Input.GetMouseButton(0))
		{ 
			var offset = Input.mousePosition - startPos;
			if (offset.magnitude > 5)
			{
				isDraggingCamera = true;
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			isDraggingCamera = false;
		}
	}*/
	
		private void CheckCharMoved()
		{
			if (mainCamera.transform.position != lastPos)
			{
				playerIsMoving = true;
			}
			else
				playerIsMoving = false;
 
			lastPos = mainCamera.transform.position;
		}
	
		private void CheckRotatingAction()
		{
			if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
			{
				startPos = Input.mousePosition;
			} 
			if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
			{ 
				var offset = Input.mousePosition - startPos;
				if (offset.magnitude > 5)
				{
					isRotatingCamera = true;
				}
			}

			if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
			{
				isRotatingCamera = false;
			}
		}
	}
}