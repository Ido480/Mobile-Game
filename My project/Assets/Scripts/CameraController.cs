using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 lookDelta;
    private float zoomDelta;
    private Vector2 moveInput;

    [Header("Camera Settings")]
    public Transform cameraPivot; // This is the object that rotates up/down
    public float lookSensitivity = 1.5f;
    public float zoomSpeed = 10f;
    public float moveSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    private float verticalAngle = 0f; // Tracks up/down angle

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Camera.Look.performed += ctx => lookDelta = ctx.ReadValue<Vector2>();
        controls.Camera.Look.canceled += _ => lookDelta = Vector2.zero;

        controls.Camera.Zoom.performed += ctx => zoomDelta = ctx.ReadValue<float>();
        controls.Camera.Zoom.canceled += _ => zoomDelta = 0f;

        controls.Camera.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Camera.Move.canceled += _ => moveInput = Vector2.zero;
    }

    private void OnEnable() => controls.Camera.Enable();
    private void OnDisable() => controls.Camera.Disable();

    private void Update()
    {
        // Horizontal rotation (left/right) around Y axis
        transform.Rotate(Vector3.up, lookDelta.x * lookSensitivity * Time.deltaTime, Space.World);

        // Vertical rotation (up/down) on pivot object
        if (cameraPivot)
        {
            verticalAngle -= lookDelta.y * lookSensitivity * Time.deltaTime;
            verticalAngle = Mathf.Clamp(verticalAngle, -60f, 60f); // Limit up/down rotation
            cameraPivot.localRotation = Quaternion.Euler(verticalAngle, 0f, 0f);

            // Zoom
            float currentZoom = cameraPivot.localPosition.z;
            currentZoom += zoomDelta * zoomSpeed * Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, -maxZoom, -minZoom);
            cameraPivot.localPosition = new Vector3(0f, 0f, currentZoom);
        }

        // Move the whole rig
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }
}
