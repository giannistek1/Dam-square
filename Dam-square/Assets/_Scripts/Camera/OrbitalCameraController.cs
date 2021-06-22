using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using _Scripts.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = System.Diagnostics.Debug;

public class OrbitalCameraController : MonoBehaviour
{
    public Transform cameraTransform;
    [Header("Object references")]
    public GameObject rightIndicator;
    public GameObject leftIndicator;
    public GameObject topIndicator;
    public GameObject bottomIndicator;
    
    [Header("Movement")]
    public float normalSpeed = 0.5f;
    public float fastSpeed = 1f;
    public float movementSpeed = 0.5f;
    public float movementTime = 2;
    public float borderPadding = 8;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    [Header("Rotation")]
    public float rotationAmount = 0.2f;
    [Header("Zoom")]
    public Vector3 zoomAmount;
    public float minZoom;
    public float maxZoom;

    [Header("Runtime values")]
    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;
    
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;
    
    private bool crRunning = false;
    
    #region Singleton
    private static OrbitalCameraController instance;
    public static OrbitalCameraController Instance
    {
        get { return instance; }
    }
    #endregion

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

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    void Update()
    {
        if (GameState.Instance.hasLoggedIn)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                HandleMouseInput();
            }
                
            HandleKeyboardInput();  
        }
    }

    void HandleMouseInput()
    {
        HandleMouseZoom();

        HandleMouseMovement();

        HandleMouseRotation();
    }

    private void HandleMouseZoom()
    {
        // Zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += zoomAmount * (Input.mouseScrollDelta.y * 10);

            // Cancel the zoom if the new y (height) goes past the limit, so you dont move the z axis
            // Since zooming moves the z and y axis
            if (newZoom.y < minZoom || newZoom.y > maxZoom)
            {
                newZoom -= zoomAmount * (Input.mouseScrollDelta.y * 10);
            }
        }
    }

    private void HandleMouseMovement()
    {
        // Border movement
        // Right
        if (Input.mousePosition.x >= Screen.width - borderPadding)
        {
            newPosition += transform.right * movementSpeed;
        }
        // Left
        if (Input.mousePosition.x <= 0 + borderPadding)
        {
            newPosition += -transform.right * movementSpeed;
        }
        // Up
        if (Input.mousePosition.y >= Screen.height - borderPadding)
        {
            newPosition += transform.forward * movementSpeed;
        }
        // Down
        if (Input.mousePosition.y <= 0 + borderPadding)
        {
            newPosition += -transform.forward * movementSpeed;
        }
    }

    private void HandleMouseRotation()
    {
        // Dragging rotation
        if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            // -difference so you drag the other way
            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
    }

    void HandleKeyboardInput()
    {
        // Change speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        } else {
            movementSpeed = normalSpeed;
        }

        // Movement
        newPosition += transform.forward * (Input.GetAxis("Vertical") * movementSpeed);
        newPosition += transform.right * (Input.GetAxis("Horizontal") * movementSpeed);

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
        
        // Zoom in
        // Todo: (Needs to be redone)
        if (Input.GetKey(KeyCode.R))
        {
            if (newZoom.y > minZoom)
                newZoom += zoomAmount;
        } // out
        if (Input.GetKey(KeyCode.F))
        {
            if (newZoom.y < maxZoom)
                newZoom -= zoomAmount;
        }

        // Limit x, z movement
        newPosition = new Vector3(Mathf.Clamp(newPosition.x, minX, maxX), newPosition.y, Mathf.Clamp(newPosition.z, minZ, maxZ));
        CheckPosition();
            
        // Apply movement, rotation, zoom
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }

    private void CheckPosition()
    {
        //TODO: More efficient since this gets called everytime
        if (newPosition.x <= minX)
            rightIndicator.SetActive(false);
        else
            rightIndicator.SetActive(true);
        
        if (newPosition.x >= maxX)
            leftIndicator.SetActive(false);
        else
            leftIndicator.SetActive(true);
        
        if (newPosition.z >= maxZ)
            bottomIndicator.SetActive(false);
        else
            bottomIndicator.SetActive(true);
        
        if (newPosition.z <= minZ)
            topIndicator.SetActive(false);
        else
            topIndicator.SetActive(true);
    }

    public void StartMoveTowardsTarget(GameObject target)
    {
        StopAllCoroutines();
        StartCoroutine(MoveTowardsTarget(target));
    }
    
    public void StartCloseZoomIn(GameObject target)
    {
        StartCoroutine(ZoomInCoroutine(target));
    }

    IEnumerator MoveTowardsTarget(GameObject target)
    {
        while ((transform.position - target.transform.position).magnitude > 1f)
        {
            newPosition = target.transform.position;
            yield return null;
        }
    }

    IEnumerator ZoomInCoroutine(GameObject target)
    {
        
        // Zoom
        newZoom += zoomAmount * (1f * 0.5f);
        if (newZoom.y < minZoom || newZoom.y > maxZoom)
        {
            newZoom -= zoomAmount * (1f * 0.5f);
        }
        
        while(newZoom.y > minZoom)
        {
            newZoom += zoomAmount * (1f * 0.5f);

            yield return null;
        }
        
        if (newZoom.y < minZoom || newZoom.y > maxZoom)
        {
            newZoom -= zoomAmount * (1f * 0.5f);
        }
    }
}
