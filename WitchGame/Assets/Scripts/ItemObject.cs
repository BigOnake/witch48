using UnityEngine;
using UnityEngine.Events;

public class ItemObject : MonoBehaviour
{
    public Item Item;

    public UnityEvent<Item> UpdatedItemState;

    public void SetNewItem(Item newItem)
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

    private void UpdateVisuals(Item newItem)
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
