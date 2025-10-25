using UnityEngine;
using UnityEngine.Events;

public class ItemCrate : MonoBehaviour
{ 
    public GameObject ItemToSpawn;
    GameObject TakeItemFromCrate()
    {
        GameObject givingObject = Instantiate(ItemToSpawn);

        return givingObject;
    }

    public void TakeItemCheck(GameObject playerHeldItem)
    {
        if (playerHeldItem != null) { return; }

        playerHeldItem = TakeItemFromCrate();
    }
}
