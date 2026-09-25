using UnityEngine;
using UnityEngine.InputSystem;
public class MouseLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;
    [Header("Player")]
    public Transform playerBody;
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
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}