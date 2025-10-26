
using UnityEngine;

public class LegAnimationHandler : MonoBehaviour
{
    private Rigidbody playerRB;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRB.linearVelocity.magnitude > 0.1f)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }
}
