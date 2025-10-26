using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class WorkshopTool : MonoBehaviour
{
    public string ToolName = "Workbench";

    [Header("Item Results - Should be same length")]
    public ItemSO[] UsableItemsOnMachine;
    public ItemSO[] ResultedItems;

    [HideInInspector] public ItemObject currentItemObjectInUse;

    [Header("Events")]
    public UnityEvent<ItemObject> PlacedNewItem;
    public event Action UseItem;
    public UnityEvent TookItem;

    ItemHolder itemHolder;

    private void Awake()
    {
        PlacedNewItem.AddListener(NewItemToUse);
        TookItem.AddListener(RemoveItem);
    }

    private void Start()
    {
        itemHolder = GetComponent<ItemHolder>();
        itemHolder?.TakeItem.AddListener(RemoveItem);
        itemHolder?.PlaceItem.AddListener(NewItemToUse);

        if (UsableItemsOnMachine.Length < 1 || ResultedItems.Length < 1)
        {
            Debug.LogWarning(ToolName + " does NOT have a list for either usable items or resulting items. Please add some");
        }
    }

    void NewItemToUse(ItemObject itemObject)
    {
        Debug.Log("New item has been placed!");
        currentItemObjectInUse = itemObject;

        if (currentItemObjectInUse != null)
        {
            UseItem?.Invoke();
        }
        
    }
    

    public void ConvertItem()
    {
        if (UsableItemsOnMachine.Length < 1 || ResultedItems.Length < 1)
        {
            Destroy(currentItemObjectInUse.gameObject);
            currentItemObjectInUse = null;
            Debug.Log("Item has been destroyed as the compatibility list hasn't been setup.");
            return;
        }

        if (!UsableItemsOnMachine.Contains(currentItemObjectInUse.Item))
        {
            Destroy(currentItemObjectInUse.gameObject);
            currentItemObjectInUse = null;
            Debug.Log("Item has been destroyed as it wasn't usable on this machine.");
            return;
        }

        int itemIndex = Array.IndexOf(UsableItemsOnMachine, currentItemObjectInUse.Item);

        ItemSO newItem;

        if (itemIndex >= ResultedItems.Length)
        {
            newItem = ResultedItems.Last();
        } else
        {
            newItem = ResultedItems[itemIndex];
        }

        Debug.Log("Item " + newItem.Name + " has been created.");    

        currentItemObjectInUse.UpdatedItemState.Invoke(newItem);
        
    }

    void RemoveItem()
    {
        currentItemObjectInUse = null;
    }


}
