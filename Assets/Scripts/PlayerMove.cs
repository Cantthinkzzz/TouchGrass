using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{

    public float moveSpeed;
    public bool setToMove = false;
    private Vector2 movementVector;
    private Vector3 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(setToMove)
        {
            moveDirection = new Vector3(movementVector.x, 0.0f, movementVector.y);
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
        }
    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        if(movementVector.magnitude > 0.1f)
        {
            setToMove = true;
        }
        else setToMove = false;
        
    }

    public void OnEscape(InputValue value)
    {
        if (value.isPressed)
        {
            GameStateManager.instance.TogglePause();
        }
    }



}
