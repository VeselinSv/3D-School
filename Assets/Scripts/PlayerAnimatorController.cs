using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerMovement movement;
    private Animator animator;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();

        if (!animator)
            Debug.LogError("PlayerAnimator НЕ е намерен! Провери дали Animator е на child object.");
    }

    void Update()
    {
        UpdateLocomotion();
        UpdateJump();
    }


    void UpdateLocomotion()
    {
        float speed = movement.HorizontalVelocity.magnitude;
        bool isMoving = speed > 0.1f;

        animator.SetBool("isGrounded", movement.IsGrounded);
        animator.SetBool("isCrouching", movement.IsCrouching);
        animator.SetBool("isSprinting", movement.IsSprinting);

        animator.SetBool("isWalking",
            isMoving &&
            !movement.IsCrouching &&
            !movement.IsSprinting
        );

        animator.SetBool("isCrouchWalking",
            isMoving &&
            movement.IsCrouching
        );

        animator.SetBool("isWalkingBack",
            Input.GetKey(KeyCode.S) &&
            !movement.IsCrouching &&
            !movement.IsSprinting
        );
    }


    void UpdateJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            movement.IsGrounded &&
            !movement.IsCrouching)
        {
            if (movement.IsSprinting)
                animator.SetTrigger("runJump");
            else
                animator.SetTrigger("jump");
        }
    }
}
