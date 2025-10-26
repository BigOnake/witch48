using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Interact : MonoBehaviour
{
    public float playerActivateDistance = 5f;

    public GameObject P_ItemHolder;
    public GameObject heldItem = null;
    public static IInteractable currentInteractable;

    public static event Action OnInteract;

    private void Update()
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, playerActivateDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                return;
            }
        }

        currentInteractable = null;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (currentInteractable == null) { return; }

        currentInteractable.Interact(this);
        OnInteract?.Invoke(GameObject CorrectInteractable);
    }

    public void PlaceOrTake(InputAction.CallbackContext context)
    {
        if (currentInteractable == null) { return; }

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
