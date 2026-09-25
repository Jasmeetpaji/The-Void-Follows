using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float crouchSpeed = 2.5f;
    [Header("Crouching")]
    public float normalHeight = 2f;
    public float crouchHeight = 1f;
    [Header("Gravity")]
    public float gravity = -9.81f;
    private CharacterController controller;
    private float verticalVelocity;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("Player needs a CharacterController component!");
        }
    }
    void Update()
    {
        if (Keyboard.current == null)
            return;
        float horizontal = 0f;
        float vertical = 0f;
        if (Keyboard.current.aKey.isPressed)
            horizontal -= 1f;
        if (Keyboard.current.dKey.isPressed)
            horizontal += 1f;
        if (Keyboard.current.sKey.isPressed)
            vertical -= 1f;
        if (Keyboard.current.wKey.isPressed)
            vertical += 1f;
        Vector3 move = transform.right * horizontal +
                       transform.forward * vertical;
        move = Vector3.ClampMagnitude(move, 1f);
        float currentSpeed;
        if (Keyboard.current.leftCtrlKey.isPressed)
        {
            currentSpeed = crouchSpeed;
            controller.height = crouchHeight;
        }
        else if (Keyboard.current.leftShiftKey.isPressed)
        {
            currentSpeed = sprintSpeed;
            controller.height = normalHeight;
        }
        else
        {
            currentSpeed = walkSpeed;
            controller.height = normalHeight;
        }
        controller.Move(
            move * currentSpeed * Time.deltaTime
        );
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        verticalVelocity += gravity * Time.deltaTime;
        controller.Move(
            Vector3.up * verticalVelocity * Time.deltaTime
        );
    }
}