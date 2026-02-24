using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashSpeed = 8f;
    [SerializeField] private float dashTime = 2f;
    private PlayerInputAction playerInputAction;
    private CharacterController characterController;
    private Vector2 moveInput;
    private Vector3 movementDirection;
    private float currentSpeed;
    private bool isDashing;
    private float dashTimer;
    

    private void OnEnable()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
        
        playerInputAction.Player.Dash.performed += DashHandler;
        playerInputAction.Player.Movement.performed += MovementPerformedHandler;
        playerInputAction.Player.Movement.canceled += MovementCanceledHandler;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentSpeed = speed;
    }

    private void Update()
    {
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (isDashing)
        {
            currentSpeed = dashSpeed;
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
                currentSpeed = speed;
            }
        }
        
        if (movementDirection.magnitude > 0)
        {
            characterController.Move(movementDirection * currentSpeed * Time.deltaTime);
        }
    }
    
    private void OnDisable()
    {
        playerInputAction.Player.Dash.performed -= DashHandler;
        playerInputAction.Player.Movement.performed -= MovementPerformedHandler;
        playerInputAction.Player.Movement.canceled -= MovementCanceledHandler;
        
        playerInputAction.Disable();
    }


    private void DashHandler(InputAction.CallbackContext context)
    {
        if (!isDashing)
        {
            isDashing = true;
            dashTimer = dashTime;
        }
    }
    
    private void MovementPerformedHandler(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    private void MovementCanceledHandler(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
}
