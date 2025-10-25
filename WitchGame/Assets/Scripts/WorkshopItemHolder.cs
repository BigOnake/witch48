using UnityEngine;

public class WorkshopItemHolder : MonoBehaviour
{
    WorkshopTool workshopTool;
    GameObject itemHolder;
    GameObject HeldItem;

    private void Start()
    {
        workshopTool = GetComponent<WorkshopTool>();

        if (workshopTool == null)
        {
            Debug.LogWarning("WorkshopItemHolder cannot find WorkshopTool. Are they inside the same gameObject?");
        }

        itemHolder = gameObject.transform.Find("Item Holder").gameObject;

        if (itemHolder == null)
        {
            Transform trans = gameObject.transform;
            itemHolder = new GameObject(name = "Item Holder");
            itemHolder.transform.parent = trans;
            Debug.LogWarning("WorkshopItemHolder cannot find a child object named 'Item Holder'. Temporary one has been made, but be sure to add one after debugging.");
        }
    }

    public void SetNewItemOnWorkshop(GameObject gameobject)
    {
        ItemObject itemObj = gameobject.GetComponent<ItemObject>();

        if (itemObj == null)
        {
            return;
        }

        gameobject.transform.position = itemHolder.transform.position;
        gameobject.transform.parent = itemHolder.transform;

        HeldItem = gameobject;

        workshopTool.PlacedNewItem.Invoke(itemObj);
    }

    public GameObject TakeItemFromWorkshop()
    {
        if (HeldItem == null)
        {
            return null;
        }

        GameObject givingObject = HeldItem;
        HeldItem = null;

        workshopTool.TookItem.Invoke();
        
        return givingObject;
    }
}
