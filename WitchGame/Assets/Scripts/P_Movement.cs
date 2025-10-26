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
    private Camera cam;
    private Vector3 camF;
    private Vector3 camR;

    [Header("Movement Values")]
    public Vector3 displaySpeed;
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

    private void Start()
    {
        cam = Camera.main;
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

    private bool UpdateInput()
    {
        wishVector = new Vector3(iMovementVector.normalized.x, 0f, iMovementVector.normalized.y) * speed; // Get correct xyz values
        return !(iMovementVector == Vector2.zero);
    }
    #endregion

    #region Movement
    private void HorizontalMovement()
    {
        previousVelocity = playerRb.linearVelocity;
        displaySpeed = playerRb.linearVelocity;

        if (!UpdateInput())
        {
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            return;
        }

        playerVelocity = adjustVec(wishVector) - previousVelocity; // Subtract for const speed. without subtracting introduces acc.
        playerVelocity = Vector3.ClampMagnitude(playerVelocity, maxSpeed); // Cap speed
        playerRb.AddForce(playerVelocity, ForceMode.Impulse);
    }

    private Vector3 adjustVec(Vector3 wishVec)
    {
        camF = cam.transform.forward;
        camR = cam.transform.right;

        camF.y = 0f;
        camR.y = 0f;

        camF.Normalize();
        camR.Normalize();

        Vector3 vec = (camF * wishVec.z) + (camR * wishVec.x);
        return new Vector3(vec.x, 0, vec.z);
    }
    #endregion
}
