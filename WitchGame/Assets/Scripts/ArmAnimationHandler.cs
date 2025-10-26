using UnityEngine;

public class ArmAnimationHandler : MonoBehaviour
{
    public Animator animator;
    public InputController inputController;
    bool isInteracting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInteracting = false;
    }

    // Update is called once per frame
    void Update()
    {
        isInteracting=Input.GetKey(KeyCode.E);
        //Debug.Log("Change Input Here");

        if (isInteracting)
        {
            animator.SetBool("isInteracting", true);
        }
        else
        {
            animator.SetBool("isInteracting", false);
        }
    }
}
