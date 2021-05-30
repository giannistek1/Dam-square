using UnityEngine;

namespace _Scripts
{
    public class ObjectPlacer : MonoBehaviour
    {
        #region Singleton
        private static ObjectPlacer instance;
        public static ObjectPlacer Instance
        {
            get { return instance; }
        }
        #endregion

        public GameObject currentPlaceableObject;
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
            if (currentPlaceableObject != null)
            {
                ReleaseGameobjectWhenClicked();

                if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
                {
                    Destroy(currentPlaceableObject);
                    bottomNavigation.SetActive(true);
                    TutorialManager.Instance.AddObjectDeletionTutorialStep();
                }
            }
            else
            {
                HandleSelectExistingObject();
            }
        }

        void FixedUpdate()
        {
            if (currentPlaceableObject != null)
            {
                MoveCurrentObjectToMouse();
                RotateFromMouseWheel();
                RotateFromKeybindings();
            }
        }

        private void ReleaseGameobjectWhenClicked()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentPlaceableObject.GetComponent<PlacingState>().placeable)
                {
                    PlaceObject();
                }
            }
        }

        private void PlaceObject()
        {
            // Use reference instead of getcomponent to optimize
            currentPlaceableObject.GetComponent<PlacingState>().placed = true;
            currentPlaceableObject.GetComponent<Collider>().isTrigger = false;
            currentPlaceableObject.GetComponent<Rigidbody>().isKinematic = false;
            currentPlaceableObject.GetComponent<Rigidbody>().useGravity = true;

            currentPlaceableObject.gameObject.layer = LayerMask.NameToLayer("Placeable");
            currentPlaceableObject.transform.SetParent(container);

            currentPlaceableObject = null;
            bottomNavigation.SetActive(true);
            TutorialManager.Instance.AddDropzoneTutorialStep();
        }

        private void MakeObjectPlaceable()
        {
            //print("Picking up object");
            currentPlaceableObject.GetComponent<PlacingState>().placed = false;
            currentPlaceableObject.GetComponent<Collider>().isTrigger = true;
            currentPlaceableObject.GetComponent<Rigidbody>().isKinematic = true;
            currentPlaceableObject.GetComponent<Rigidbody>().useGravity = false;

            currentPlaceableObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            TutorialManager.Instance.AddObjectSelectionTutorialStep();
        }

        private void RotateFromMouseWheel()
        {
            if (rotateFromMouseWheel)
            {
                mouseWheelRotation += Input.mouseScrollDelta.y;
                currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
            }
        }

        private void RotateFromKeybindings()
        {
            if (rotateFromKeybindings)
            {
                if (Input.GetKey(rotateLeft))
                {
                    currentPlaceableObject.transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
                    TutorialManager.Instance.AddObjectRotationTutorialStep();
                }
                else if (Input.GetKey(rotateRight))
                {
                    currentPlaceableObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                    TutorialManager.Instance.AddObjectRotationTutorialStep();
                }
                // currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
            }

        }

        private void MoveCurrentObjectToMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                //Vector3 objectBounds = currentPlaceableObject.GetComponentInChildren<Renderer>().bounds.max;
                currentPlaceableObject.transform.position = new Vector3(hitInfo.point.x, 0.2f, hitInfo.point.z);

                // if (currentPlaceableObject.GetComponent<PlacingState>().placeable)
                //     currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

                // else
                //     currentPlaceableObject.transform.rotation = Quaternion.identity;
            }
        }

        public void HandleNewObject(GameObject newObject)
        {
            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = Instantiate(newObject);
                bottomNavigation.SetActive(false);
            }
        }

        private void HandleSelectExistingObject()
        {
            if (currentPlaceableObject == null)
            {
                if (!Input.GetMouseButtonDown(0)) return;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask mask = LayerMask.GetMask("Placeable");
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, mask))
                {
                    if (hitInfo.collider.gameObject.CompareTag("Placeable"))
                    {
                        currentPlaceableObject = hitInfo.collider.gameObject;
                        MakeObjectPlaceable();
                    }
                }
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

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
