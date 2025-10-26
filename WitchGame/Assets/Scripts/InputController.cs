// Attach this script to the game object that needs to recieve inputs

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public InputSystem_Actions inputSysActions;

    #region InputActions
    // Variables to hold reference to input system's actions
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction interactAction;
    private InputAction getItemAction;
    #endregion

    #region Events
    // Invoked when a input system actions is updated or gets a correct value
    public static event Action<Vector2> onPlayerMove;
    public static event Action<Vector2> onPlayerLook;
    public static event Action onPlayerInteract;
    public static event Action onPlayerGetItem;
    #endregion

    private Vector2 movementInputs;
    private Vector2 lookingInputs;

    private void OnEnable()
    {
        Debug.Log("<color=green> Input script is enabled. </color>");
        i_EnableInputs();
    }

    private void OnDisable()
    {
        Debug.Log("<color=red> Input script is disabled. </color>");
        i_DisableInputs();
    }

    private void Awake()
    {
        inputSysActions = new InputSystem_Actions();

        moveAction = inputSysActions.Player.Move;
        lookAction = inputSysActions.Player.Look;
        interactAction = inputSysActions.Player.Interact;
        getItemAction = inputSysActions.Player.GetItem;
    }

    private void Update()
    {
        ReadMovementInputs();
        ReadLookingInputs();
    }

    private void i_EnableInputs()
    {
        interactAction.performed += Interact;
        getItemAction.performed += GetItem;

        moveAction.Enable();
        lookAction.Enable();
        inputSysActions.Player.Interact.Enable();
    }

    private void i_DisableInputs()
    {
        interactAction.performed -= Interact;
        getItemAction.performed -= GetItem;

        moveAction.Disable();
        lookAction.Disable();
        inputSysActions.Player.Interact.Disable();
    }

    #region Invokes
    private void GetItem(InputAction.CallbackContext context)
    {
        onPlayerGetItem?.Invoke();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        onPlayerInteract?.Invoke();
    }

    private void ReadMovementInputs()
    {
        movementInputs = moveAction.IsPressed() ? moveAction.ReadValue<Vector2>(): Vector2.zero;
        onPlayerMove?.Invoke(movementInputs);
    }

    private void ReadLookingInputs()
    {
        lookingInputs = lookAction.ReadValue<Vector2>();
        onPlayerLook?.Invoke(lookingInputs);
    }
    #endregion
}
