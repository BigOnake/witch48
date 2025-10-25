using UnityEngine;
using UnityEngine.Events;

public class ItemCrate : MonoBehaviour
{ 
    public GameObject ItemToSpawn;

    private void Start()
    {
        P_Interact.OnItemPlaceOrTake += TakeItemCheck;
    }
    GameObject TakeItemFromCrate()
    {
        GameObject givingObject = Instantiate(ItemToSpawn);


        return givingObject;
    }

    public void TakeItemCheck(GameObject playerHeldItem, GameObject playerItemHolder, GameObject CorrectInteractable)
    {
        if (CorrectInteractable != gameObject)
        {
            return;
        }

        if (playerHeldItem != null) { return; }

        playerHeldItem = TakeItemFromCrate();
        playerHeldItem.transform.parent = playerItemHolder.transform;
    }
}
