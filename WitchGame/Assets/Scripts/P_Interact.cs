using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class P_Interact : MonoBehaviour
{
    public float playerActivateDistance = 5f;

    public GameObject P_ItemHolder;
    public GameObject heldItem = null;
    public static IInteractable currentInteractable;

    public static event Action<GameObject> OnInteract;

    private void Update()
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.root.right, out hit, playerActivateDistance))
        {
            Debug.DrawRay(transform.position, (hit.point - transform.position), Color.red); //remove
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                return;
            }
        }

        Debug.DrawRay(transform.position, transform.root.right * playerActivateDistance, Color.green); //remove
        currentInteractable = null;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("owner:" + context.action.name);
        if (currentInteractable == null) { return; }
        if (context.action.inProgress) { return; }

        currentInteractable.Interact(this);
        GameObject obj = (currentInteractable as MonoBehaviour).gameObject;
        OnInteract?.Invoke(obj);
    }

    public void PlaceOrTake(InputAction.CallbackContext context)
    {
        if (currentInteractable == null) { return; }
        if (context.action.inProgress) { return; }

        currentInteractable.PlaceOrTake(this);
    }

    public void SetHeldItem(GameObject newItem)
    {
        if (heldItem != null) { return; }

        heldItem = newItem;
        heldItem.transform.parent = P_ItemHolder.transform;
        heldItem.transform.localPosition = Vector3.zero;
    }

    public GameObject RetrieveHeldItem()
    {
        if (heldItem == null) { return null; }

        GameObject temp = heldItem;
        heldItem = null;
        temp.transform.parent = null;
        return temp;
    }

    public void DestroyHeldItem()
    {
       if (heldItem != null)
        {
            Destroy(heldItem);
            heldItem = null;
        }
    }

    public bool IsHoldingItem()
    {
        return heldItem != null;
    }
}
