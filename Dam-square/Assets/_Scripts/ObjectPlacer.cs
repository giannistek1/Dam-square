using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    #region Singleton
    public static ObjectPlacer instance;
    public static ObjectPlacer Instance
    {
        get { return instance; }
    }
    #endregion

    public GameObject currentPlacableObject;
    public GameObject bottomNavigation;

    [SerializeField] private bool rotateFromMouseWheel;
    [SerializeField] private bool rotateFromKeybindings;
    [SerializeField] private KeyCode rotateLeft;
    [SerializeField] private KeyCode rotateRight;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Transform container;

    float mouseWheelRotation;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        // If has object
        if (currentPlacableObject != null)
        {
            ReleaseGameobjectWhenClicked();

            if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
            {
                Destroy(currentPlacableObject);
                bottomNavigation.SetActive(true);
            }
        }
        
        HandleSelectExistingObject();
    }

    void FixedUpdate()
    {
        if (currentPlacableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            RotateFromKeybindings();
        }
    }

    public void CleanScene()
    {
        if (container.childCount > 0)
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
    }

    private void ReleaseGameobjectWhenClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentPlacableObject.GetComponent<PlacingState>().placable)
            {
                PlaceObject();
            }
        }
    }

    private void PlaceObject()
    {
        print("Object placed!");
        // Use reference instead of getcomponent to optimize
        currentPlacableObject.GetComponent<PlacingState>().placed = true;
        currentPlacableObject.GetComponent<Collider>().isTrigger = false;
        currentPlacableObject.GetComponent<Rigidbody>().isKinematic = false;
        currentPlacableObject.GetComponent<Rigidbody>().useGravity = true;

        currentPlacableObject.gameObject.layer = LayerMask.NameToLayer("Placable");
        currentPlacableObject.transform.SetParent(container);

        currentPlacableObject = null;
        bottomNavigation.SetActive(true);
    }

    private void MakeObjectPlacable()
    {
        print("Picking up object");
        currentPlacableObject.GetComponent<PlacingState>().placed = false;
        currentPlacableObject.GetComponent<Collider>().isTrigger = true;
        currentPlacableObject.GetComponent<Rigidbody>().isKinematic = true;
        currentPlacableObject.GetComponent<Rigidbody>().useGravity = false;

        currentPlacableObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    private void RotateFromMouseWheel()
    {
        if (rotateFromMouseWheel)
        {
            mouseWheelRotation += Input.mouseScrollDelta.y;
            currentPlacableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        }
    }

    private void RotateFromKeybindings()
    {
        if (rotateFromKeybindings)
        {
            if (Input.GetKey(rotateLeft))
            {
                currentPlacableObject.transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(rotateRight))
            {
                currentPlacableObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
            // currentPlacableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
        }

    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            //Vector3 objectBounds = currentPlacableObject.GetComponentInChildren<Renderer>().bounds.max;
            currentPlacableObject.transform.position = new Vector3(hitInfo.point.x, 0.5f, hitInfo.point.z);

            // if (currentPlacableObject.GetComponent<PlacingState>().placable)
            //     currentPlacableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            // else
            //     currentPlacableObject.transform.rotation = Quaternion.identity;
        }
    }

    public void HandleNewObject(GameObject newObject)
    {
        if (currentPlacableObject != null)
        {
            Destroy(currentPlacableObject);
        }
        else
        {
            currentPlacableObject = Instantiate(newObject);
            bottomNavigation.SetActive(false);
        }
    }

    private void HandleSelectExistingObject()
    {
        if (currentPlacableObject == null)
        {
            if (!Input.GetMouseButtonDown(0)) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask mask = LayerMask.GetMask("Placable");
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, mask))
            {
                if (hitInfo.collider.gameObject.CompareTag("Placable"))
                {
                    currentPlacableObject = hitInfo.collider.gameObject;
                    MakeObjectPlacable();
                }
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
