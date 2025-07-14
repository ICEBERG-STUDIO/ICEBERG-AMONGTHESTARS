using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Player Attributes")] [SerializeField]
    private GameObject _player;

    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private int _maxJump = 1;
    [SerializeField] private float _dashForce = 2f;
    private int jumpNumber = 0;
    private bool jumpPressed = false;
    
    [Space] [Header("Layers : ")] [SerializeField]
    public LayerMask _groundLayer;
    [SerializeField] public Transform _groundCheck;
    public float groundCheckRadius = 0.2f;
    private bool isGrounded = true;
    
    [Space]
    [Header("External Attributes :")]
    [SerializeField] private TextMeshProUGUI _interactKeyText;

    private Vector2 mMoveVector;
    private Vector2 direction;
    private Rigidbody2D rgbd2D;
    private bool canInteract;
    IInteractable iInteractable;

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

        isGrounded = Physics2D.OverlapCircle(_groundCheck.position, groundCheckRadius, _groundLayer);
        if (jumpPressed && isGrounded)
        {
            Jump();
        }

        if (isGrounded)
        {
            jumpNumber = 0;
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
            if (jumpNumber < _maxJump)
            {
                jumpPressed = true;
                jumpNumber += 1;
            }
            else if (context.canceled)
            {
                jumpPressed = false;
            }
        }
    }

    public void RedInteractInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canInteract)
            {
                Interact();
            }
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

    #endregion

    public void Move()
    {
        // Find the direction
        direction = new Vector2(mMoveVector.x, mMoveVector.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Apply the movement
            rgbd2D.position += direction * _speed;

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
        rgbd2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        jumpPressed = false;
    }

    public void Dash()
    {
        rgbd2D.position += (direction * _dashForce);
    }

    public void Interact()
    {
        iInteractable.Interact();
        ToogleInteractionKeyUiVisibility();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.unityLogger.Log(other.name + " is triggered");
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            canInteract = true;
            iInteractable = interactable;
            string textContent = GameManager.GetJsonTextValue("Interaction", true);
            _interactKeyText.text = textContent;
            ToogleInteractionKeyUiVisibility();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.unityLogger.Log(other.name + " is triggered");
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            canInteract = false;
            iInteractable = null;
            _interactKeyText.gameObject.SetActive(false);
        }
    }

    private void ToogleInteractionKeyUiVisibility()
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
}