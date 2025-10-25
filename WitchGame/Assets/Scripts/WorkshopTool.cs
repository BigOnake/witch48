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
    public Item[] UsableItemsOnMachine;
    public Item[] ResultedItems;

    public static ItemObject currentItemObjectInUse;

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
    }

    void NewItemToUse(ItemObject itemObject)
    {
        currentItemObjectInUse = itemObject;
        
        UseItem.Invoke();
    }
    

    public void ConvertItem()
    {

        if (!UsableItemsOnMachine.Contains(currentItemObjectInUse.Item))
        {
            Destroy(currentItemObjectInUse.gameObject);
            currentItemObjectInUse = null;
            return;
        }

        int itemIndex = Array.IndexOf(UsableItemsOnMachine, currentItemObjectInUse.Item);

        Item newItem;

        if (itemIndex >= ResultedItems.Length)
        {
            newItem = ResultedItems.Last();
        } else
        {
            newItem = ResultedItems[itemIndex];
        }

            

        currentItemObjectInUse.UpdatedItemState.Invoke(newItem);
        
    }

    void RemoveItem()
    {
        currentItemObjectInUse = null;
    }


}
