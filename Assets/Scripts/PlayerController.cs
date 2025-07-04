using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Player Attributes")] [SerializeField]
    private GameObject mPlayer;

    [SerializeField] private float mSpeed = 3.0f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int maxJump = 1;
    [SerializeField] private float dashForce = 2f;
    private int JumpNumber = 0;
    private bool jumpPressed = false;

    [Space] [Header("Layers : ")] [SerializeField]
    public LayerMask groundLayer;

    [SerializeField] public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded = true;

    private Vector2 mMoveVector;
    private Vector2 direction;
    private Rigidbody2D rgbd2D;

    #endregion

    #region Initialization

    private void Awake()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        Destroy(this);
    }

    #endregion

    private void FixedUpdate()
    {
        Move();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (jumpPressed && isGrounded)
        {
            Jump();
        }

        if (isGrounded)
        {
            JumpNumber = 0;
        }
    }

    #region Read Inputs

    public void ReadMoveInput(InputAction.CallbackContext context)
    {
        mMoveVector = context.ReadValue<Vector2>();
    }

    public void ReadJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (JumpNumber < maxJump)
            {
                jumpPressed = true;
                JumpNumber += 1;
            }
            else if (context.canceled)
            {
                jumpPressed = false;
            }
        }
    }

    public void ReadDashnput(InputAction.CallbackContext context)
    {
        if (context.performed)
            Dash();
    }

    #endregion

    public void Move()
    {
        // Find the direction
        direction = new Vector2(mMoveVector.x, mMoveVector.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Apply the movement
            rgbd2D.position += direction * mSpeed;

            //m_Animator.SetBool("isWalkin", true);
        }
        else
        {
            // If the character don't move, set the isWalkin parameter to false
            //m_Animator.SetBool("isWalkin", false);
        }
    }

    public void Jump()
    {
        // Apply jump force if grounded
        rgbd2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpPressed = false;
    }

    public void Dash()
    {
        rgbd2D.position += (direction * dashForce);
    }
}