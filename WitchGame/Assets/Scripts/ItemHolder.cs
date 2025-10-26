using UnityEngine;
using UnityEngine.Events;

public class ItemHolder : MonoBehaviour, IInteractable
{
    GameObject heldItem;
    protected GameObject itemHolder;

    public UnityEvent<ItemObject> PlaceItem;
    public UnityEvent TakeItem;

    private void Start()
    {
        itemHolder = transform.Find("Item Holder").gameObject;

        if (itemHolder == null)
        {
            itemHolder = new GameObject(name = "Item Holder");
            itemHolder.transform.parent = transform;
            Debug.LogWarning("WorkshopItemHolder cannot find a child object named 'Item Holder'. Temporary one has been made, but be sure to add one after debugging.");
        }
    }

    public void Interact(P_Interact player)
    {
        return;
    }

    public void PlaceOrTake(P_Interact player)
    {
        if (heldItem == null)
        {
            if (player.IsHoldingItem())
            {
                SetNewItemOnHolder(player.RetrieveHeldItem());
            }
        }
        else
        {
            GameObject temp = player.RetrieveHeldItem();
            player.SetHeldItem(TakeItemFromHolder());
            SetNewItemOnHolder(temp);
            
        }
    }

    private void SetNewItemOnHolder(GameObject item)
    {
        if (item == null) { return; }

        ItemObject itemObj = item.GetComponent<ItemObject>();

        if (itemObj == null) { return; }

        item.transform.parent = itemHolder.transform;
        item.transform.localPosition = Vector3.zero;

        heldItem = item;

        PlaceItem?.Invoke(itemObj);
    }

    private GameObject TakeItemFromHolder()
    {
        if (heldItem == null)
        {
            return null;
        }

        GameObject givingObject = heldItem;
        heldItem = null;

        TakeItem?.Invoke();

        return givingObject;
    }
}
