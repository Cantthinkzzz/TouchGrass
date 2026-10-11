using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool setToMove = false;
    private Vector2 movementVector;
    private Vector3 moveDirection;

    [SerializeField]
    private float interactionDistance = 50.0f;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private Animator animator;

    void Start()
    {
        // If animator wasn't assigned in the inspector, try to grab it automatically
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        if (setToMove)
        {
            moveDirection = new Vector3(movementVector.x, 0.0f, movementVector.y);

            transform.position += moveSpeed * Time.fixedDeltaTime * moveDirection;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }

        // Update the animator with the movement magnitude (0 when idle, up to 1 when moving)
        if (animator != null)
        {
            float currentSpeed = setToMove ? movementVector.magnitude : 0f;
            animator.SetFloat("Speed", currentSpeed);
            // Note: Make sure your Animator has a float parameter named "Speed"
        }
    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        if (movementVector.magnitude > 0.1f)
        {
            setToMove = true;
        }
        else
        {
            setToMove = false;
        }
    }

    public void OnEscape(InputValue value)
    {
        if (value.isPressed)
        {
            GameStateManager.instance.TogglePause();
        }
    }
}