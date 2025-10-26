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

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

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

        SetItemSprite(Item.ItemIcon);
    }

    void SetItemSprite(Sprite sprite)
    {
        if (sprite == null) { Debug.LogWarning("A sprite for " + Item.name + " does not exist."); return; }

        spriteRenderer.sprite = sprite;
    }
}
