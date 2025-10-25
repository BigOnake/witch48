using UnityEngine;
using UnityEngine.Events;

public class ItemHolder : MonoBehaviour
{
    GameObject itemHolder;
    GameObject HeldItem;

    public UnityEvent<ItemObject> PlaceItem;
    public UnityEvent TakeItem;

    private void Start()
    {

        itemHolder = gameObject.transform.Find("Item Holder").gameObject;

        if (itemHolder == null)
        {
            Transform trans = gameObject.transform;
            itemHolder = new GameObject(name = "Item Holder");
            itemHolder.transform.parent = trans;
            Debug.LogWarning("WorkshopItemHolder cannot find a child object named 'Item Holder'. Temporary one has been made, but be sure to add one after debugging.");
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

        HeldItem = gameobject;

        PlaceItem.Invoke(itemObj);
    }

    private GameObject TakeItemFromHolder()
    {
        if (HeldItem == null)
        {
            return null;
        }

        GameObject givingObject = HeldItem;
        HeldItem = null;

        TakeItem.Invoke();

        return givingObject;
    }

    // Allows player to swap out items if needed.
    public void TakeOrPlaceItemCheck(GameObject playerHeldItem)
    {
        if (HeldItem == null)
        {
            SetNewItemOnHolder(playerHeldItem);
        } else
        {
            var temp = playerHeldItem;
            playerHeldItem = TakeItemFromHolder();
            SetNewItemOnHolder(temp);
        }
    }
}
