using UnityEngine;
using UnityEngine.InputSystem;
public class MouseLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 0.15f;
    [Header("Player")]
    public Transform playerBody;
    [Header("Look Limits")]
    public float minLookAngle = -90f;
    public float maxLookAngle = 90f;
    private float verticalRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Mouse.current == null)
            return;
        Vector2 mouseDelta =
            Mouse.current.delta.ReadValue();
        float mouseX =
            mouseDelta.x * mouseSensitivity;
        float mouseY =
            mouseDelta.y * mouseSensitivity;
        verticalRotation -= mouseY;
        verticalRotation =
            Mathf.Clamp(
                verticalRotation,
                minLookAngle,
                maxLookAngle
            );
        transform.localRotation =
            Quaternion.Euler(
                verticalRotation,
                0f,
                0f
            );
        if (playerBody != null)
        {
            playerBody.Rotate(
                Vector3.up * mouseX
            );
        }
    }
}