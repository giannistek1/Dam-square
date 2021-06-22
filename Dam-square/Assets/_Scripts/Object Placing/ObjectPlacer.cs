using System;
using _Scripts.Game;
using _Scripts.Localization;
using _Scripts.UI;
using TMPro;
using UnityEngine;

namespace _Scripts.Object_Placing
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
        
        public Transform container;
        public GameObject currentPlaceableObject;
        public GameObject objectContainerClass;
        public HUD hud;
        public ParticleSystem smokeEffect;

        private GameObject bottomNavigation;
        private TextMeshProUGUI feedbackText;
        private LTDescr delay;

        private Vector3 previousObjectPosition;
        [SerializeField] private bool rotateFromKeybindings;
        [SerializeField] private KeyCode rotateLeft;
        [SerializeField] private KeyCode rotateRight;
        [SerializeField] private float rotationSpeed;

        float mouseWheelRotation;

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
        }

        private void Start()
        {
            bottomNavigation = hud.bottomNavigation;
            feedbackText = hud.feedbackText;
            
            // Create object container
            GameObject objectContainer = Instantiate(objectContainerClass);
            container = objectContainer.transform;
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
                    
                    feedbackText.gameObject.SetActive(true);
                    feedbackText.text = LocalizationManager.GetTextByKey("OBJECT_DELETED");
                    feedbackText.color = Color.green;
                    delay = LeanTween.delayedCall(2f, () =>
                    {
                        feedbackText.gameObject.SetActive(false);
                    });
                }
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
                    
                    // Only destroy if you didn't click on it
                    if(previousObjectPosition == new Vector3(999,999,999))
                        Destroy(currentPlaceableObject);
                    else
                    {
                        currentPlaceableObject.transform.position = previousObjectPosition;
                        currentPlaceableObject.GetComponent<PlacingState>().SetStandardMaterials();
                        ResetObject();
                    }

                    //feedbackText.gameObject.SetActive(true);
                    //feedbackText.text = LocalizationManager.GetTextByKey("OBJECT_DELETED");
                    //feedbackText.color = Color.red;
                    //delay = LeanTween.delayedCall(2f, () =>
                    //{
                    //    feedbackText.gameObject.SetActive(false);
                    //});
                    
                    bottomNavigation.SetActive(true);
                }
            }
            else
            {
                SelectExistingObject();
            }
        }

        void FixedUpdate()
        {
            if (currentPlaceableObject != null)
            {
                MoveCurrentObjectToMouse();
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
                else
                {
                    SoundManager.PlaySound(SoundManager.Sound.ObjectCannotBePlaced);
                    
                    //TODO: Disambiguate between dropzone/object feedback
                    feedbackText.gameObject.SetActive(true);
                    feedbackText.text = LocalizationManager.GetTextByKey("CANT_PLACE_HERE");
                    feedbackText.color = Color.red;
                    delay = LeanTween.delayedCall(2f, () =>
                    {
                        feedbackText.gameObject.SetActive(false);
                    });
                }
            }
        }

        private void PlaceObject()
        {
            Vector3 particlePosition = currentPlaceableObject.GetComponent<Renderer>().bounds.center;
            particlePosition.y = 0.05f;
            smokeEffect.transform.position = particlePosition;
            smokeEffect.Play();
            SoundManager.PlaySound(SoundManager.Sound.ObjectPlaced);
            
            ResetObject();
            
            feedbackText.gameObject.SetActive(true);
            feedbackText.text = LocalizationManager.GetTextByKey("OBJECT_PLACED");
            feedbackText.color = Color.green;
            delay = LeanTween.delayedCall(2f, () =>
            {
                feedbackText.gameObject.SetActive(false);
            });
            
            bottomNavigation.SetActive(true);
            
            if (!GameState.Instance.isInTutorial)
                hud.deleteAllButton.SetActive(true);
            
            TutorialManager.Instance.AddDropzoneTutorialStep();
        }

        private void PickUpObject()
        {
            SoundManager.PlaySound(SoundManager.Sound.ObjectPickedUp);

            currentPlaceableObject.GetComponent<PlacingState>().placed = false;
            currentPlaceableObject.GetComponent<Collider>().isTrigger = true;
            currentPlaceableObject.GetComponent<Rigidbody>().isKinematic = true;
            currentPlaceableObject.GetComponent<Rigidbody>().useGravity = false;

            currentPlaceableObject.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            TutorialManager.Instance.AddObjectSelectionTutorialStep();
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

        private void SelectExistingObject()
        {
            // Pick up object
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
                        previousObjectPosition = currentPlaceableObject.transform.position;
                        PickUpObject();
                    }
                }
            }
        }

        private void ResetObject()
        {
            //TODO: Use reference instead of getcomponent to optimize
            currentPlaceableObject.GetComponent<PlacingState>().placed = true;
            currentPlaceableObject.GetComponent<Collider>().isTrigger = false;
            currentPlaceableObject.GetComponent<Rigidbody>().isKinematic = false;
            currentPlaceableObject.GetComponent<Rigidbody>().useGravity = true;

            currentPlaceableObject.gameObject.layer = LayerMask.NameToLayer("Placeable");
            currentPlaceableObject.transform.SetParent(container);
            
            currentPlaceableObject = null;
            previousObjectPosition = new Vector3(999,999,999);
        }
    
        public void CleanScene()
        {
            //TODO: Can be faster if you use your own list
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Placeable");
            
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
            hud.deleteAllButton.SetActive(false);
            
            feedbackText.gameObject.SetActive(true);
            feedbackText.text = LocalizationManager.GetTextByKey("DELETED_ALL_OBJECTS");
            feedbackText.color = Color.green;
            delay = LeanTween.delayedCall(2f, () =>
            {
                feedbackText.gameObject.SetActive(false);
            });
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
