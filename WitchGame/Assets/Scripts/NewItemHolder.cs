using UnityEngine;
using UnityEngine.Events;

public class NewItemHolder : AbstractItemHolder
{
    GameObject heldItem;
    protected GameObject itemHolder;

    public UnityEvent TakeItem;

    protected override void Start()
    {
        base.Start();
        itemHolder = transform.Find("Item Holder").gameObject;

        if (itemHolder == null)
        {
            itemHolder = new GameObject(name = "Item Holder");
            itemHolder.transform.parent = transform;
            Debug.LogWarning("WorkshopItemHolder cannot find a child object named 'Item Holder'. Temporary one has been made, but be sure to add one after debugging.");
        }
    }

    public override void HandlePlayerInteraction(GameObject playerHeldItem, GameObject playerItemHolder, GameObject CorrectInteractable)
    {
        if (CorrectInteractable != gameObject)
        {
            return;
        }

        if (heldItem == null)
        {
            SetNewItemOnHolder(playerHeldItem);
        }
        else
        {
            var temp = playerHeldItem;
            playerHeldItem = TakeItemFromHolder();
            playerHeldItem.transform.parent = playerItemHolder.transform;
            SetNewItemOnHolder(temp);
        }
    }

    private void SetNewItemOnHolder(GameObject gameobject)
    {
        ItemObject itemObj = gameobject.GetComponent<ItemObject>();

        if (itemObj == null)
        {
            return;
        }

        gameobject.transform.position = itemHolder.transform.position;
        gameobject.transform.parent = itemHolder.transform;

        heldItem = gameobject;

        PlaceItem.Invoke(itemObj);
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
