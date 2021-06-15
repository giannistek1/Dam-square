using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Assertions;

public class PlacingState : MonoBehaviour
{
    public bool placeable = false;
    public bool placed = false;
    public bool withinObject = false;
    public Transform[] collisionPoints;

    [SerializeField] Material canNotBePlacedMat;

    Material[] canNotBePlacedMats;

    private Material[] standardMaterials;
    private new Renderer renderer;
    private void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();
        standardMaterials = new Material[renderer.materials.Length];

        Assert.IsNotNull(canNotBePlacedMat);
        canNotBePlacedMats = new Material[renderer.materials.Length];

        for (int i = 0; i < renderer.materials.Length; i++)
        {
            standardMaterials[i] = renderer.materials[i];
            
            canNotBePlacedMats[i] = canNotBePlacedMat;
        }
        
        renderer.materials = canNotBePlacedMats;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!placed && other.gameObject.CompareTag("Placeable"))
        {
            placeable = false;
            
            renderer.materials = canNotBePlacedMats;
            //Debug.Log(placeable);
        }
        
        else if (!placed && !withinObject && other.gameObject.CompareTag("Dropzone"))
        {
            OrbitalCameraController.Instance.StartMoveTowardsTarget(other.gameObject);
            OrbitalCameraController.Instance.StartCloseZoomIn(other.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!placed && other.gameObject.CompareTag("Placeable"))
        {
            withinObject = false;
            placeable = true;

            renderer.materials = standardMaterials;

            //Debug.Log(placeable);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!placed && other.gameObject.CompareTag("Placeable"))
        {
            withinObject = true;
            placeable = false;

            if (canNotBePlacedMat != null)
            {
                renderer.materials = canNotBePlacedMats;
            }
            //Debug.Log(placeable);
        }
        else if (!placed && !withinObject && other.gameObject.CompareTag("Dropzone"))
        {
            foreach (Transform collisionpoint in collisionPoints)
            {
                if (!collisionpoint.GetComponent<CollisionPoint>().isInDropzone)
                {
                    placeable = false;

                    if (canNotBePlacedMat != null)
                    {
                        renderer.materials = canNotBePlacedMats;
                    }
                    return;
                }
            }
            placeable = true;

            renderer.materials = standardMaterials;
        }

    }
    
    void OnMouseEnter() {
        //print("Mouse over object");
    }
    
    void OnMouseExit() {
    
    }
}
