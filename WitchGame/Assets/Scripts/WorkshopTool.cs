using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WorkshopTool : MonoBehaviour
{
    public string ToolName = "Workbench";
    
    public ItemState NewItemState;

    ItemObject currentItemObjectInUse;

    public bool requiresPlayerInput = false;
    float playerCurrentDuration = 0f;

    public float ToolUseDurationInSeconds = 5f;

    bool isDone = false;

    public UnityEvent<ItemObject> PlacedNewItem;

    private void Start()
    {
        NewItemToUse(new ItemObject());
    }

    void NewItemToUse(ItemObject itemObject)
    {
        currentItemObjectInUse = itemObject;

        isDone = false;

        if (requiresPlayerInput)
        {
            StartCoroutine(SetAutoTimer());
        } else
        {
            playerCurrentDuration = 0;
        }
    }

    

    IEnumerator SetAutoTimer()
    {
        yield return new WaitForSeconds(ToolUseDurationInSeconds);

        ConvertItem();
    }

    public void OnPlayerInteractTool()
    {
        playerCurrentDuration += 0.1f;

        if (playerCurrentDuration >= ToolUseDurationInSeconds) {
            ConvertItem();
        }
    }

    void ConvertItem()
    {
        isDone = true;

        if (currentItemObjectInUse.GetItemState() != ItemState.RAW)
        {
            Destroy(currentItemObjectInUse.gameObject);
            currentItemObjectInUse = null;
            return;
        }

        currentItemObjectInUse.UpdatedItemState.Invoke(NewItemState);
        
    }

    void PlaceNewItem(ItemObject newItem)
    {
        if (!currentItemObjectInUse)
        {
            PlacedNewItem.Invoke(newItem);
        }
    }

    public void GetItemFromWorkshop()
    {
        if (!isDone)
        {
            if (requiresPlayerInput)
            {
                isDone = true;
                // Give Item GameObject
            }
        } else
        {
            // Give Item GameObject
        }
    }


}
