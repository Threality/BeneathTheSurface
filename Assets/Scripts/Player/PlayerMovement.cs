using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, InputSystem_Actions.IPlayerMovementActions
{
    #region Setup

    private Animator animator;

    // Allow for editing variables in the inspector
    [Header("Base Movement Settings")]
    public float maxSpeed;
    public float maxDownwardSpeed;
    public float acceleration;
    public float deceleration;
    public bool isFlipped;

    [Header("Jump Settings")]
    public float jumpForce;
    public int defaultCoyote;
    public float sdownwardMultiplication;

    [Header("Colliders")]
    [SerializeField] public GroundCheck groundCheck;

    // Set up hidden values 
    private float coyoteTime = 0;
    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;
    private float defaultGravity;

    void OnEnable()
    {
        // Connect player input and rigidbody to script
        PlayerInputManager.Instance.playerControls.PlayerMovement.Enable();
        PlayerInputManager.Instance.playerControls.PlayerMovement.SetCallbacks(this);
    }

    void OnDisable()
    {
        // Disconnect input to prevent null references
        PlayerInputManager.Instance.playerControls.PlayerMovement.Disable();
        PlayerInputManager.Instance.playerControls.PlayerMovement.RemoveCallbacks(this);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
        animator = GetComponent<Animator>();
    }

    #endregion

 
    #region Movement


    void Update()
    {
        // Check grounded state
        coyoteTime = groundCheck.IsGrounded() ? defaultCoyote : coyoteTime;


        // Move the player
        if (horizontalInput == 0)
        {
            // If no player input - Decelerate
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, 0, deceleration * Time.deltaTime);
        }
        else
        {
            // If player input - Move
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, maxSpeed * horizontalInput, acceleration * Time.deltaTime);
        }

        if (isFlipped)
        {
            rb.gravityScale = -defaultGravity;
            float mult = verticalInput < 0 ? sdownwardMultiplication * verticalInput: 1f;
            rb.linearVelocityY += (mult - 1) * Time.deltaTime;
            if (rb.linearVelocityY > maxDownwardSpeed * mult)
            {
                //If above speed cap - Reduce speed
                rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, maxDownwardSpeed, deceleration * Time.deltaTime);
            }
        }
        else
        {
            rb.gravityScale = defaultGravity;
            float mult = verticalInput < 0 ? sdownwardMultiplication * verticalInput: 1f;
            rb.linearVelocityY -= (mult - 1) * Time.deltaTime;
            if (rb.linearVelocityY < -1 * maxDownwardSpeed * mult)
            {
                //If above speed cap - Reduce speed
                rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, -1 * maxDownwardSpeed, deceleration * Time.deltaTime);
            }
        }


    }

    void FixedUpdate()
    {
        // Reduce coyote time
        coyoteTime = coyoteTime < 0 ? coyoteTime : coyoteTime - 1;
    }


    #endregion
    #region Input


    public void OnHorizontalMove(InputAction.CallbackContext context)
    {
        // Read the players horizontal input
        horizontalInput = context.ReadValue<float>();
    }

    public void OnVerticalMove(InputAction.CallbackContext context)
    {
        // Read the players horizontal input
        verticalInput = context.ReadValue<float>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            rb.linearVelocityY = rb.linearVelocityY / 2f;
            return;
        }
        
        // If the player is currently or was recently
        if (coyoteTime >= 0)
        {
            float mult = isFlipped ? -1 : 1;
            rb.linearVelocityY = jumpForce * mult;
        }
        coyoteTime = -1;
    }

    public void OnFlip(InputAction.CallbackContext context)
    {
        if (groundCheck.IsOnBaseplate())
        {
            GameManager.instance.ChangePlayerState(GameManager.instance.GetPlayerState());
        }
    }


    #endregion


}