using UnityEngine;
using UnityEngine.Events;

public abstract class AbstractItemHolder : MonoBehaviour
{
    public UnityEvent<ItemObject> PlaceItem;

    protected virtual void Start()
    {
        P_Interact.OnItemPlaceOrTake += HandlePlayerInteraction;
    }

    public abstract void HandlePlayerInteraction(GameObject playerHeldItem, GameObject playerItemHolder, GameObject CorrectInteractable);
}
