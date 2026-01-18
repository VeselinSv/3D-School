using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController controller;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 12f;
    [SerializeField] private float crouchSpeed = 6f;
    [SerializeField] private float sprintSpeed = 18f;
    [SerializeField] private float gravity = -19.62f;
    [SerializeField] private float jumpHeight = 3f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Crouch")]
    [SerializeField] private float crouchHeight = 0.5f;
    private float originalHeight;

    [Header("Ceiling Check")]
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private float ceilingDistance = 0.25f;
    [SerializeField] private LayerMask ceilingMask;

    [Header("Sprint / Stamina")]
    [SerializeField] private float maxStamina = 5f;
    [SerializeField] private float staminaDrain = 1f;
    [SerializeField] private float staminaRegen = 0.7f;
    [SerializeField] private float regenDelay = 1.5f;
    [SerializeField] private StaminaBar staminaUI;

    public bool IsGrounded { get; private set; }
    public bool IsCrouching { get; private set; }
    public bool IsSprinting { get; private set; }
    public Vector3 HorizontalVelocity { get; private set; }

    private float stamina;
    private float regenTimer;
    private Vector3 verticalVelocity;

    void Awake()
    {
        if (!controller)
            controller = GetComponent<CharacterController>();

        originalHeight = controller.height;
        stamina = maxStamina;
        UpdateStaminaUI();
    }

    void Update()
    {
        CheckGround();
        HandleInput();
        HandleMovement();
        HandleGravity();
        HandleStamina();
        UpdateStaminaUI();
    }


    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            ToggleCrouch();

        if (Input.GetKeyDown(KeyCode.Space) && IsCrouching)
            ToggleCrouch();

        if (Input.GetKeyDown(KeyCode.LeftControl))
            TryStartSprint();

        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopSprinting();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded && !IsCrouching)
            Jump();
    }


    void HandleMovement()
    {
        float x = (Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f);
        float z = (Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f);

        Vector3 move = transform.right * x + transform.forward * z;
        move = Vector3.ClampMagnitude(move, 1f);

        float speed = walkSpeed;
        if (IsCrouching) speed = crouchSpeed;
        else if (IsSprinting) speed = sprintSpeed;

        HorizontalVelocity = move * speed;
        controller.Move(HorizontalVelocity * Time.deltaTime);
    }


    void HandleGravity()
    {
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    void Jump()
    {
        verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void CheckGround()
    {
        IsGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        if (IsGrounded && verticalVelocity.y < 0f)
            verticalVelocity.y = -2f;
    }


    void ToggleCrouch()
    {
        if (IsCrouching && IsCeilingBlocked())
            return;

        IsCrouching = !IsCrouching;
        controller.height = IsCrouching ? crouchHeight : originalHeight;

        if (IsCrouching && IsSprinting)
            StopSprinting();
    }

    bool IsCeilingBlocked()
    {
        return Physics.CheckSphere(
            ceilingCheck.position,
            ceilingDistance,
            ceilingMask
        );
    }


    void TryStartSprint()
    {
        if (stamina > 0f && !IsCrouching)
            IsSprinting = true;
    }

    void StopSprinting()
    {
        if (!IsSprinting) return;
        IsSprinting = false;
        regenTimer = regenDelay;
    }

    void HandleStamina()
    {
        if (IsSprinting)
        {
            stamina -= staminaDrain * Time.deltaTime;
            if (stamina <= 0f)
            {
                stamina = 0f;
                StopSprinting();
            }
        }
        else
        {
            if (regenTimer > 0f)
                regenTimer -= Time.deltaTime;
            else if (stamina < maxStamina)
                stamina += staminaRegen * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    }

    void UpdateStaminaUI()
    {
        if (staminaUI)
            staminaUI.SetStamina(stamina, maxStamina);
    }
}
