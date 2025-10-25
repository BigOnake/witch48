using UnityEngine;
using UnityEngine.InputSystem;

public class T_Movement : MonoBehaviour
{
    #region Fields
    private Rigidbody playerRb;
    private Vector2 iMovementVector;
    private Vector3 wishVector;
    private Vector3 playerVelocity;
    private Vector3 previousVelocity;

    [Header("Movement Values")]
    public float displaySpeed;
    public float speed = 8f;
    public float maxSpeed = 15f;
    #endregion

    #region GameEngineLoop
    private void OnEnable()
    {
        //Debug.Log("<color=green> Movement script is enabled. </color>");
        //InputController.onPlayerMove += ReadInputs;
    }

    private void OnDisable()
    {
        //Debug.Log("<color=red> Movement script is disabled. </color>");
        //InputController.onPlayerMove -= ReadInputs;
    }

    private void Awake()
    {
        playerRb = GetComponentInParent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HorizontalMovement();
    }
    #endregion

    #region Inputs
    public void ReadInputs(InputAction.CallbackContext context)
    {
        iMovementVector = context.action.IsPressed()? context.ReadValue<Vector2>() : Vector2.zero;
    }

    private Vector3? UpdateInput()
    {
        wishVector = new Vector3(iMovementVector.x, 0f, iMovementVector.y) * speed; // Get correct xyz values
        return wishVector;
    }
    #endregion

    #region Movement
    private void HorizontalMovement()
    {
        previousVelocity = playerRb.linearVelocity;

        UpdateInput();
        playerVelocity = (wishVector - previousVelocity);
        playerVelocity = new Vector3(playerVelocity.x, 0, playerVelocity.z); //Dont apply vertical forces
        playerVelocity = Vector3.ClampMagnitude(playerVelocity, maxSpeed); // Cap speed

        playerRb.AddForce(playerVelocity, ForceMode.Impulse);

        if(playerRb.linearVelocity.magnitude < 0.5f) // Instant break
        {
            playerRb.linearVelocity = Vector3.zero;
        }

        displaySpeed = playerRb.linearVelocity.magnitude;
    }
    #endregion
}
