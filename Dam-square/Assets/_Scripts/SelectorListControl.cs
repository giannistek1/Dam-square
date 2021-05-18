using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts;
using UnityEngine;
using UnityEngine.UI;

public class SelectorListControl : MonoBehaviour
{
    #region Fields
    public GameObject objectSelectorPrefab;
    public GameObject scrollRect;

    [SerializeField] PlaceableObject[] placableObjects;
    private List<GameObject> allObjectSelectors;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        allObjectSelectors = new List<GameObject>();

        for (int i = 0; i < placableObjects.Length; i++)
        {
            GameObject newObject = Instantiate(objectSelectorPrefab);
            newObject.GetComponent<ObjectSelector>().placeableObject = placableObjects[i];
            newObject.transform.SetParent(gameObject.transform);
            allObjectSelectors.Add(newObject);
        }
    }
    #endregion

    public void Filter(ToggleButton toggleButton)
    {
        List<GameObject> newList = allObjectSelectors.Where(o => o.GetComponent<ObjectSelector>().placeableObject.category == toggleButton.objectCategory).ToList();

        HideAllSelectors();

        for (int i = 0; i < newList.Count; i++)
        {
            newList[i].SetActive(true);
        }
        scrollRect.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }

    private void HideAllSelectors()
    {
        for (int i = 0; i < allObjectSelectors.Count; i++)
        {
            allObjectSelectors[i].SetActive(false);
        }
    }

    public void ShowAllSelectors()
    {
        for (int i = 0; i < allObjectSelectors.Count; i++)
        {
            allObjectSelectors[i].SetActive(true);
        }
        scrollRect.GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }
}
