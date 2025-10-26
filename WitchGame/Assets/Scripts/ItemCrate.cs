using UnityEngine;

public class ItemCrate : MonoBehaviour, IInteractable
{ 
    public GameObject ItemToSpawn;

    public void Interact(P_Interact player)
    {
        return;
    }

    public void PlaceOrTake(P_Interact player)
    {
        if (player.IsHoldingItem())
        {
            return;
        }

        GameObject givingObject = Instantiate(ItemToSpawn);
        player.SetHeldItem(givingObject);
    }
}
