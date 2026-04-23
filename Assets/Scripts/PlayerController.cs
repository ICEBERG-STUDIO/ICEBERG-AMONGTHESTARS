using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Player Attributes")] 
    [SerializeField] private GameObject _player;

    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _swimSpeed = 1.0f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private int _maxJump = 1;
    [SerializeField] private float _dashForce = 2f;
    [SerializeField] private float _waterGravityScale = 0.05f;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _spriteObject;
    
    // Life
    [SerializeField] private int maxLife;
    
    [ProgressBar("Health", nameof(maxLife), EColor.Red)] [SerializeField]
    private int _currentHealth;
    
    private int jumpNumber = 0;
    private bool canJump = true;
    private bool wasGroundedLastFrame = true;
    private float normalGravity;
    private float normalSpeed;
    private float normalJumpForce;

    [Header("Layers : ")] 
    [SerializeField] public LayerMask _groundLayer;
    [SerializeField] public Transform _groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded = true;

    [Header("External Attributes :")]
    [SerializeField] private TextMeshProUGUI _interactKeyText;

    [Space]
    
    private Vector2 mMoveVector;
    private Vector2 direction;
    private Rigidbody2D rgbd2D;
    private bool canInteract;
    IInteractable iInteractable;
    private bool isInWater = false;

    #endregion

    #region Initialization

    private void Awake()
    {
        _currentHealth = maxLife;
        rgbd2D = GetComponent<Rigidbody2D>();
        normalGravity = rgbd2D.gravityScale;
        normalSpeed = _speed;
        normalJumpForce = _jumpForce;
    }
    
    private void OnDisable()
    {
        Destroy(this);
    }

    #endregion

    private void FixedUpdate()
    {
        Move();

        // Ground check
        bool isCurrentlyGrounded = Physics2D.OverlapCircle(_groundCheck.position, groundCheckRadius, _groundLayer);
        if (isCurrentlyGrounded && !wasGroundedLastFrame)
        {
            jumpNumber = 0;
            canJump = true;
        }

        isGrounded = isCurrentlyGrounded;
        wasGroundedLastFrame = isCurrentlyGrounded;

        if (isInWater)
        {
            ApplyWaterDrag();
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
            if (jumpNumber < _maxJump && canJump)
            {
                Jump();
            }
        }
    }

    public void ReadInteractInput(InputAction.CallbackContext context)
    {
        if (context.performed && canInteract)
        {
            Interact();
        }
    }

    public void ReadDashnput(InputAction.CallbackContext context)
    {
        if (context.performed)
            Dash();
    }

    public void ReadSonnarInput(InputAction.CallbackContext context)
    {
        Debug.Log(" input");
        if (context.performed)
        {
            Debug.Log("sonnar input");
            Sonnar();
        }
    }

    public void ReadFruitsInput(InputAction.CallbackContext context)
    {
        Debug.Log(" input");
        if (context.performed)
        {
            FruitsActivate();
        }
    }

    #endregion

    public void Move()
    {
        direction = new Vector2(mMoveVector.x, mMoveVector.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if (!isInWater)
                direction.y = 0;
            
            _spriteObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer);
            if (direction.x < 0.0f)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            rgbd2D.position += direction * _speed;
            
            if(isGrounded)
                _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }
    }

    public void Jump()
    {
        jumpNumber ++;
        canJump = false;
        rgbd2D.AddForce(Vector2.up * normalJumpForce, ForceMode2D.Impulse);
    }

    public void Dash()
    {
        rgbd2D.position += (direction * _dashForce);
    }

    public void Interact()
    {
        iInteractable.Interact();
        ToggleInteractionKeyUiVisibility();
    }

    public void TakeDamage(int ReceivedDamage)
    {
        _currentHealth -= ReceivedDamage;
        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    public void RefillLife()
    {
        _currentHealth = maxLife;
    }
    
    public void Death()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        transform.position = new Vector3(playerData.data.LastPlayerPosition.LastCheckpointPositionX, playerData.data.LastPlayerPosition.LastCheckpointPositionY, playerData.data.LastPlayerPosition.LastCheckpointPositionZ);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //On trigger enter
        if (other.TryGetComponent<IOnTrigger>(out var enter))
        {
            enter.OnEnter();
        }

        //Interact
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            canInteract = true;
            iInteractable = interactable;
            string textContent = GameManager.GetJsonTextValue("Interaction", true);
            _interactKeyText.text = textContent;
            ToggleInteractionKeyUiVisibility();
        }

        if (other.CompareTag("Water"))
        {
            EnterWater();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        //On trigger exit
        if (other.TryGetComponent<IOnTrigger>(out var exit))
        {
            exit.OnExit();
        }

        //Interact
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            canInteract = false;
            iInteractable = null;
            _interactKeyText.gameObject.SetActive(false);
        }

        if (other.CompareTag("Water"))
        {
            ExitWater();
        }
    }

    private void ToggleInteractionKeyUiVisibility()
    {
        _interactKeyText.gameObject.SetActive(!_interactKeyText.gameObject.activeSelf);
    }

    #region Sonnar

    [Space]
    [Header("Sonnar")]
    [SerializeField] private SonnarPlayer _sonnarPlayer;


    public void Sonnar()
    {
        _sonnarPlayer.ActivateSonnar();
    }
    #endregion

    #region Fruits

    [Space]
    [Header("Fruits")]
    [SerializeField] private Fruits _fruitsPlayer;

    public void FruitsActivate()
    {
        _fruitsPlayer.ChangeFuits();
    }
    #endregion
    
    private void EnterWater()
    {
        isInWater = true;
        rgbd2D.gravityScale = _waterGravityScale;
        _speed = _swimSpeed;
        _jumpForce /= 10.0f;
    }

    private void ExitWater()
    {
        isInWater = false;
        rgbd2D.gravityScale = normalGravity;
        _speed = normalSpeed;
        _jumpForce = normalJumpForce;
    }

    /// <summary>
    /// Reduce Player vector Y when entering the water to simulate the water friction
    /// </summary>
    private void ApplyWaterDrag()
    {
        Vector2 velocity = rgbd2D.linearVelocity;

        if (velocity.y < -2f) // reduce speed when entering the water
        {
            velocity.y = Mathf.Lerp(velocity.y, -2f, Time.fixedDeltaTime * 2f); // Lerp(Actual Y speed, target Y speed, FixedDeltaTime * speed transition)
            rgbd2D.linearVelocity = velocity;
        }
    }
}
