using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item Item;

    ItemState State = ItemState.RAW;

    /// <summary>
    /// Get the current cooking state of the Item.
    /// </summary>
    /// <returns></returns>
    public ItemState GetItemState()
    {
        return State;
    }

    /// <summary>
    /// Set new cooking state for the item.
    /// </summary>
    /// <param name="newState"></param>
    public void SetItemState(ItemState newState)
    {
        State = newState;
    }

    private void Start()
    {
        
    }

    public void UpdateVisuals()
    {

    }
}
