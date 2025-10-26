using UnityEngine;
using UnityEngine.Events;

public class ItemObject : MonoBehaviour
{
    public ItemSO Item;

    public UnityEvent<ItemSO> UpdatedItemState;

    public void SetNewItem(ItemSO newItem)
    {
        Item = newItem;
    }

    MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();

        UpdatedItemState.AddListener(SetNewItem);
        UpdatedItemState.AddListener(UpdateVisuals);
    }

    private void Start()
    {
        UpdatedItemState.Invoke(Item);
    }

    private void UpdateVisuals(ItemSO newItem)
    {
        if (Item == null) { return; }

        SetItemMesh(Item.ItemMesh);
    }

    void SetItemMesh(Mesh mesh)
    {
        if (mesh == null) { Debug.LogWarning("A mesh for " + Item.name + " does not exist."); return; }

        meshFilter.mesh = mesh;
    }
}
