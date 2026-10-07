using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 7f;
    public float crouchSpeed = 2f;
    [Header("Acceleration")]
    public float acceleration = 10f;
    public float deceleration = 12f;
    [Header("Crouching")]
    public float normalHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchTransitionSpeed = 8f;
    [Header("Gravity")]
    public float gravity = -20f;
    [Header("Stamina")]
    public float maxStamina = 100f;
    public float stamina = 100f;
    public float sprintStaminaDrain = 20f;
    public float staminaRecovery = 15f;
    public float minimumStaminaToSprint = 10f;
    [Header("Camera")]
    public Transform playerCamera;
    [Header("Head Bob")]
    public float walkBobSpeed = 8f;
    public float walkBobAmount = 0.035f;
    public float sprintBobSpeed = 12f;
    public float sprintBobAmount = 0.06f;
    [Header("Peeking / Leaning")]
    public float peekAngle = 15f;
    public float peekSpeed = 8f;
    public float peekCameraOffset = 0.35f;
    [Header("Footstep Feel")]
    public float footstepInterval = 0.5f;
    public float sprintFootstepInterval = 0.3f;
    private CharacterController controller;
    private float verticalVelocity;
    private Vector3 currentVelocity;
    private Vector3 cameraStartPosition;
    private float bobTimer;
    private float footstepTimer;
    private float currentPeekAngle;
    private float currentPeekOffset;
    private bool isCrouching;
    private bool isSprinting;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError(
                "Player needs a CharacterController!"
            );
            return;
        }
        stamina = maxStamina;
        if (playerCamera != null)
        {
            cameraStartPosition =
                playerCamera.localPosition;
        }
        else
        {
            Debug.LogError(
                "Player Camera is not assigned!"
            );
        }
    }
    void Update()
    {
        if (Keyboard.current == null)
            return;
        HandleMovement();
        HandleCrouching();
        HandleStamina();
        HandlePeeking();
        HandleHeadBob();
    }
    void HandleMovement()
    {
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
        Vector3 inputDirection =
            transform.right * horizontal +
            transform.forward * vertical;
        inputDirection =
            Vector3.ClampMagnitude(
                inputDirection,
                1f
            );
        bool moving =
            inputDirection.magnitude > 0.1f;
        bool holdingSprint =
            Keyboard.current.leftShiftKey.isPressed;
        isSprinting =
            holdingSprint &&
            moving &&
            !isCrouching &&
            stamina > 0f;
        float targetSpeed;
        if (isCrouching)
        {
            targetSpeed = crouchSpeed;
        }
        else if (isSprinting)
        {
            targetSpeed = sprintSpeed;
        }
        else
        {
            targetSpeed = walkSpeed;
        }
        Vector3 targetVelocity =
            inputDirection * targetSpeed;
        currentVelocity =
            Vector3.Lerp(
                currentVelocity,
                targetVelocity,
                Time.deltaTime *
                (
                    moving
                        ? acceleration
                        : deceleration
                )
            );
        controller.Move(
            currentVelocity *
            Time.deltaTime
        );
        if (controller.isGrounded &&
            verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }
        verticalVelocity +=
            gravity *
            Time.deltaTime;
        controller.Move(
            Vector3.up *
            verticalVelocity *
            Time.deltaTime
        );
    }
    void HandleCrouching()
    {
        isCrouching =
            Keyboard.current.leftCtrlKey.isPressed;
        float targetHeight =
            isCrouching
                ? crouchHeight
                : normalHeight;
        controller.height =
            Mathf.Lerp(
                controller.height,
                targetHeight,
                Time.deltaTime *
                crouchTransitionSpeed
            );
    }
    void HandleStamina()
    {
        if (isSprinting)
        {
            stamina -=
                sprintStaminaDrain *
                Time.deltaTime;
        }
        else
        {
            stamina +=
                staminaRecovery *
                Time.deltaTime;
        }
        stamina =
            Mathf.Clamp(
                stamina,
                0f,
                maxStamina
            );
    }
    void HandlePeeking()
    {
        float targetAngle = 0f;
        float targetOffset = 0f;
        if (Keyboard.current.qKey.isPressed)
        {
            targetAngle = peekAngle;
            targetOffset = -peekCameraOffset;
        }
        if (Keyboard.current.eKey.isPressed)
        {
            targetAngle = -peekAngle;
            targetOffset = peekCameraOffset;
        }
        currentPeekAngle =
            Mathf.Lerp(
                currentPeekAngle,
                targetAngle,
                Time.deltaTime *
                peekSpeed
            );
        currentPeekOffset =
            Mathf.Lerp(
                currentPeekOffset,
                targetOffset,
                Time.deltaTime *
                peekSpeed
            );
        if (playerCamera == null)
            return;
        Vector3 targetPosition =
            cameraStartPosition;
        targetPosition.x +=
            currentPeekOffset;
        playerCamera.localPosition =
            Vector3.Lerp(
                playerCamera.localPosition,
                targetPosition,
                Time.deltaTime *
                peekSpeed
            );
        Quaternion targetRotation =
            Quaternion.Euler(
                0f,
                0f,
                currentPeekAngle
            );
        playerCamera.localRotation =
            Quaternion.Lerp(
                playerCamera.localRotation,
                targetRotation,
                Time.deltaTime *
                peekSpeed
            );
    }
    void HandleHeadBob()
    {
        if (playerCamera == null)
            return;
        bool moving =
            currentVelocity.magnitude > 0.1f;
        if (!moving ||
            !controller.isGrounded)
        {
            bobTimer = 0f;
            return;
        }
        float bobSpeed =
            isSprinting
                ? sprintBobSpeed
                : walkBobSpeed;
        float bobAmount =
            isSprinting
                ? sprintBobAmount
                : walkBobAmount;
        bobTimer +=
            Time.deltaTime *
            bobSpeed;
        float bobX =
            Mathf.Cos(
                bobTimer * 0.5f
            ) *
            bobAmount;
        float bobY =
            Mathf.Sin(
                bobTimer
            ) *
            bobAmount;
        Vector3 targetPosition =
            cameraStartPosition;
        targetPosition.x +=
            currentPeekOffset;
        targetPosition.x += bobX;
        targetPosition.y += bobY;
        playerCamera.localPosition =
            Vector3.Lerp(
                playerCamera.localPosition,
                targetPosition,
                Time.deltaTime *
                10f
            );
    }
}