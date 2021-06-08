using System;
using UnityEngine;

public class CollisionPoint : MonoBehaviour
{
    #region Fields
		public bool isInDropzone = false;
	#endregion
	
	#region Unity Methods
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Dropzone"))
		{
			isInDropzone = true;
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Dropzone"))
		{
			isInDropzone = false;
		}
	}
	#endregion
}
