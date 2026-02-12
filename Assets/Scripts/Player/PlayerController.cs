using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    private PlayerInputAction playerInputAction;
    private CharacterController characterController;
    private Vector2 moveInput;
    private Vector3 movementDirection;

    private void OnEnable()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
        
        playerInputAction.Player.Somersault.performed += context => DashHandler();
        playerInputAction.Player.Movement.performed += context => MovementPerformedHandler(context);
        playerInputAction.Player.Movement.canceled += context => MovementCanceledHandler();
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (movementDirection.magnitude > 0)
        {
            characterController.Move(movementDirection * speed * Time.deltaTime);
        }
    }
    
    private void OnDisable()
    {
        playerInputAction.Player.Somersault.performed -= context => DashHandler();
        playerInputAction.Player.Movement.performed -= context => MovementPerformedHandler(context);
        playerInputAction.Player.Movement.canceled -= context => MovementCanceledHandler();
        
        playerInputAction.Disable();
    }
    
    private void DashHandler()
    {
        // TODO: Implement dash logic in CK-9
    }
    
    private void MovementPerformedHandler(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    private void MovementCanceledHandler()
    {
        moveInput = Vector2.zero;
    }
}
