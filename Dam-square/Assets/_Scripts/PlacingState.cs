using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingState : MonoBehaviour
{
    public bool placable = true;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!placed && other.gameObject.CompareTag("Placable"))
        {
            placable = false;

            if (canNotBePlacedMat != null)
            {
                renderer.materials = canNotBePlacedMats;
            }

            //Debug.Log(placable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!placed && other.gameObject.CompareTag("Placable"))
        {
            placable = true;

            renderer.materials = standardMaterials;

            //Debug.Log(placable);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (!placed && other.gameObject.CompareTag("Placable"))
        {
            placable = false;

            if (canNotBePlacedMat != null)
            {
                renderer.materials = canNotBePlacedMats;
            }
            //Debug.Log(placable);
        }
    }
}
