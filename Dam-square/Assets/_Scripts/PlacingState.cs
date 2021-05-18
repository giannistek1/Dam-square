using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingState : MonoBehaviour
{
    public bool placeable = true;
    public bool placed = false;

    [SerializeField] Material canNotBePlacedMat;

    Material[] canNotBePlacedMats;

    private Material[] standardMaterials;
    private new Renderer renderer;
    private void Awake()
    {
        renderer = gameObject.GetComponent<Renderer>();
        standardMaterials = new Material[renderer.materials.Length];

        if (canNotBePlacedMat != null)
            canNotBePlacedMats = new Material[renderer.materials.Length];

        for (int i = 0; i < renderer.materials.Length; i++)
        {
            standardMaterials[i] = renderer.materials[i];

            if (canNotBePlacedMat != null)
                canNotBePlacedMats[i] = canNotBePlacedMat;
        }
        
        if (canNotBePlacedMat != null)
        {
            renderer.materials = canNotBePlacedMats;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!placed && other.gameObject.CompareTag("Placeable"))
        {
            placeable = false;

            if (canNotBePlacedMat != null)
            {
                renderer.materials = canNotBePlacedMats;
            }
            //Debug.Log(placeable);
        }

        if (!placed && other.gameObject.CompareTag("Dropzone"))
        {
            print("Dropzone entered");
            placeable = true;

            renderer.materials = standardMaterials;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!placed && other.gameObject.CompareTag("Placeable"))
        {
            placeable = true;

            renderer.materials = standardMaterials;

            //Debug.Log(placeable);
        }
        
        if (!placed && other.gameObject.CompareTag("Dropzone"))
        {
            placeable = false;

            if (canNotBePlacedMat != null)
            {
                renderer.materials = canNotBePlacedMats;
            }

            //Debug.Log(placeable);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!placed && other.gameObject.CompareTag("Placeable"))
        {
            placeable = false;

            if (canNotBePlacedMat != null)
            {
                renderer.materials = canNotBePlacedMats;
            }
            //Debug.Log(placeable);
        }
    }
    
    void OnMouseEnter() {
        print("Mouse over object");
    }
    
    void OnMouseExit() {
    
    }
}
