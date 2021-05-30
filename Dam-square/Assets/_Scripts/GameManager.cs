using System;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields
    private Vector3 startPos;
    public bool isDraggingCamera = false;
    public bool isRotatingCamera = false;
    #endregion
	
	#region Singleton
	private static GameManager instance;
	public static GameManager Instance { get { return instance; } }
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
		// Asserts
	}

	private void Start()
	{
		
	}

	void Update()
	{
		// Check for drag action
		CheckDraggingAction();

		CheckRotatingAction();
	}
	#endregion

	private void CheckDraggingAction()
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
