using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class P_Rotation : MonoBehaviour
{
    #region Fields
    private Vector2 rawInputs;
    private Vector3 lookingInputs;
    private InputDevice iDevice;
    private Camera cam;
    private Vector3 target;
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

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Look();
    }
    #endregion

    #region Inputs
    public void ReadInputs(InputAction.CallbackContext context)
    {
        iDevice = context.control.device;
        rawInputs = context.ReadValue<Vector2>();
        lookingInputs = new Vector3(rawInputs.x, 0, rawInputs.y);
        //lookingInputs = new Vector3(input.x, 0, input.y);
    }
    #endregion

    #region Movement
    public void Look()
    {
        if (lookingInputs != Vector3.zero)
        {
            if(iDevice is Pointer)
            {
                targetPos();
                Vector3 targetDir = target - transform.root.position;
                targetDir.y = 0;

                if(targetDir != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(targetDir);
                    transform.root.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y - 90, 0f); // -90 to adjust for angle dif
                }
                
            }
            if(iDevice is Gamepad)
            {
                //transform.root.rotation = Quaternion.LookRotation(-lookingInputs); //works great w/ gamepad
                Quaternion lookRotation = Quaternion.LookRotation(adjustVec(lookingInputs), Vector3.up);
                transform.root.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y - 90, 0f);
            }
        }
    }

    private void targetPos()
    {
        RaycastHit hit;
        Ray cameraRay = Camera.main.ScreenPointToRay(rawInputs);

        if (Physics.Raycast(cameraRay, out hit))
        {
            target = hit.point;
        }
    }

    private Vector3 adjustVec(Vector3 wishVec)
    {
        Vector3 camF = cam.transform.forward;
        Vector3 camR = cam.transform.right;

        camF.y = 0f;
        camR.y = 0f;

        camF.Normalize();
        camR.Normalize();

        Vector3 vec = (camF * wishVec.z) + (camR * wishVec.x);
        return new Vector3(vec.x, 0, vec.z);
    }
    #endregion
}
