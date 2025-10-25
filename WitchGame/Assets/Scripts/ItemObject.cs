using UnityEngine;
using UnityEngine.Events;

public class ItemObject : MonoBehaviour
{
    public Item Item;

    ItemState State = ItemState.RAW;

    public UnityEvent<ItemState> UpdatedItemState;

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

    MeshFilter meshFilter;

    private void Awake()
    {
        UpdatedItemState.AddListener(SetItemState);
        UpdatedItemState.AddListener(UpdateVisuals);
    }

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    public void UpdateVisuals(ItemState newState = ItemState.RAW)
    {
        if (Item == null) { return; }

        SwitchMeshes();
    }

    void SwitchMeshes()
    {
        switch(State){
            case ItemState.RAW:
                SetItemMesh(Item.RAWItemMesh);
                break;
            case ItemState.CHOPPED:
                SetItemMesh(Item.CHOPPEDItemMesh);
                break;
            case ItemState.GRINDED:
                SetItemMesh(Item.GRINDEDItemMesh);
                break;
        }
    }

    void SetItemMesh(Mesh mesh)
    {
        if (mesh == null) { Debug.LogWarning("A mesh for " + Item.name + " does not exist."); return; }

        meshFilter.mesh = mesh;
    }
}
