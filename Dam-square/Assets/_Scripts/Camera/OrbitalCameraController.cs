using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = System.Diagnostics.Debug;

public class OrbitalCameraController : MonoBehaviour
{
    public Transform cameraTransform;

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

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            HandleMouseInput();
        }
            
        HandleMovementInput();    
    }

    void HandleMouseInput()
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
        

        // Dragging movement
        /*if (Input.GetMouseButtonDown(0))
        {
            // Create plane to raycast on from camera to mouse and save that as startposition
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        // If LMB still held down
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                // Current cam position + difference
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }*/

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

            // -difference so you can drag the other way
            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
    }

    void HandleMovementInput()
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
        // Apply movement, rotation, zoom
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
