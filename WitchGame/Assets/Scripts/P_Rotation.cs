using UnityEngine;
using UnityEngine.InputSystem;

public class P_Rotation : MonoBehaviour
{
    #region Fields
    private Vector3 lookingInputs;
    public float sensitivity = 0.1f;
    #endregion

    #region GameEngineLoop
    private void OnEnable()
    {
        //Debug.Log("<color=green> Camera script is enabled. </color>");
        //InputController.onPlayerLook += ReadInputs;
    }

    private void OnDisable()
    {
        //Debug.Log("<color=red> Camera script is disabled. </color>");
        //InputController.onPlayerLook -= ReadInputs;
    }

    void Update()
    {
        Look();
    }
    #endregion

    #region Inputs
    public void ReadInputs(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        lookingInputs = new Vector3(input.x, 0, input.y);
    }
    #endregion

    #region Movement
    public void Look()
    {
        if (lookingInputs != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(-lookingInputs); //works great w/ gamepad
        }
    }
    #endregion
}
