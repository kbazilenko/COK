using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    private CharacterController characterController;
    private Vector2 moveInput;
    
    private Vector3 movementDirection;
    [SerializeField] public float speed = 5f;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        
        playerInputAction.Player.Somersault.performed += context => SomersaultHandler();
        
        playerInputAction.Player.Movement.performed += context => moveInput = context.ReadValue<Vector2>();
        playerInputAction.Player.Movement.canceled += context => moveInput = Vector2.zero;
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
    private void SomersaultHandler()
    {
    }

    private void OnEnable()
    {
        playerInputAction.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
    }
}
