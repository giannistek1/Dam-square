using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields
    public GameObject mainCamera;
    private Vector3 startPos;
    private Vector3 lastPos;
    public bool playerIsMoving = false;
    public bool isRotatingCamera = false;
    #endregion
	
	#region Singleton
	private static GameManager instance;
	public static GameManager Instance { get { return instance; } }
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
		
	}

	private void Start()
	{
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
