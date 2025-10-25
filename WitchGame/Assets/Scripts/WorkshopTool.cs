using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WorkshopTool : MonoBehaviour
{
    public string ToolName = "Workbench";
    
    public ItemState NewItemState;

    public static ItemObject currentItemObjectInUse;

    public UnityEvent<ItemObject> PlacedNewItem;
    public event Action UseItem;
    public UnityEvent TookItem;

    private void Awake()
    {
        PlacedNewItem.AddListener(NewItemToUse);
        TookItem.AddListener(RemoveItem);
    }

    void NewItemToUse(ItemObject itemObject)
    {
        currentItemObjectInUse = itemObject;
        UseItem.Invoke();
    }
    

    public void ConvertItem()
    {

        if (currentItemObjectInUse.GetItemState() != ItemState.RAW)
        {
            Destroy(currentItemObjectInUse.gameObject);
            currentItemObjectInUse = null;
            return;
        }

        currentItemObjectInUse.UpdatedItemState.Invoke(NewItemState);
        
    }

    void RemoveItem()
    {
        currentItemObjectInUse = null;
    }


}
