using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float diveSpeed = 4f;
    [SerializeField] private float diveSprintSpeed = 6f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float mouseSensitivity = 2f;

    [Header("Look Settings")]
    private float minPitch = -25f;
    private float maxPitch = 45f;

    [Header("Camera")]
    [SerializeField] private Transform cameraTransform;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 velocity;

    private bool isSprinting = false;
    private bool hasJumped = false;

    private float verticalLookRotation = 0f;
    private float yawRotation = 0f;

    private MovementMode currentMode = MovementMode.Walking;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        // Optional:
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void OnEnable()
    {
        PlayerEvents.OnChangeMovementMode += SetMovementMode;
        PlayerEvents.OnSprintToggled += SetSprint;
        PlayerEvents.OnJumpRequested += RequestJump;
    }

    private void OnDisable()
    {
        PlayerEvents.OnChangeMovementMode -= SetMovementMode;
        PlayerEvents.OnSprintToggled -= SetSprint;
        PlayerEvents.OnJumpRequested -= RequestJump;
    }

    private void Update()
    {
        // if (!GameStateManager.Instance.IsGameplay)
        //     return;

        HandleLook();

        switch (currentMode)
        {
            case MovementMode.Walking:
                HandleMovement();
                HandleJump();
                ApplyGravity();
                break;

            case MovementMode.Diving:
                HandleDivingMovement();
                velocity = Vector3.zero; 
                break;
        }

        MoveCharacter();
    }

    private void HandleLook()
    {
        Vector2 lookInput = InputManager.Instance.LookInput * mouseSensitivity;

        verticalLookRotation -= lookInput.y;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, minPitch, maxPitch);
        cameraTransform.localRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);

        yawRotation += lookInput.x;
        transform.rotation = Quaternion.Euler(0f, yawRotation, 0f);
    }

    private void HandleMovement()
    {
        Vector2 moveInput = InputManager.Instance.MoveInput;
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        Vector3 direction = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        moveDirection = direction * speed;
    }

    private void HandleJump()
    {
        if (!characterController.isGrounded)
            return;

        velocity.y = -2f;

        if (InputManager.Instance.SpaceKey)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            hasJumped = true;
        }
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }

    private void HandleDivingMovement()
    {
        Vector2 moveInput = InputManager.Instance.MoveInput;
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        Vector3 move = (forward * moveInput.y + right * moveInput.x).normalized;
        float speed = isSprinting ? diveSprintSpeed : diveSpeed;

        moveDirection = move * speed;
    }

    private void MoveCharacter()
    {
        Vector3 finalMove = moveDirection;
        finalMove.y += velocity.y;
        characterController.Move(finalMove * Time.deltaTime);
    }

    // EVENT HANDLERS
    private void SetMovementMode(MovementMode mode)
    {
        currentMode = mode;
    }

    private void SetSprint(bool sprinting)
    {
        isSprinting = sprinting;
    }

    private void RequestJump()
    {
        hasJumped = true;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    // Optional external access
    // public void ForceSetMode(MovementMode mode)
    // {
    //     SetMovementMode(mode);
    // }

    public MovementMode GetMovementMode()
    {
        return currentMode;
    }
}
