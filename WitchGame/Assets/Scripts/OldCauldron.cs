//using System.Linq;
//using UnityEngine;
//using UnityEngine.Events;

//public class OldCauldron : MonoBehaviour
//{
//    GameObject itemHolder;
//    GameObject[] HeldItems;

//    private void Start()
//    {

//        itemHolder = gameObject.transform.Find("Item Holder").gameObject;

//        if (itemHolder == null)
//        {
//            Transform trans = gameObject.transform;
//            itemHolder = new GameObject(name = "Item Holder");
//            itemHolder.transform.parent = trans;
//            Debug.LogWarning("WorkshopItemHolder cannot find a child object named 'Item Holder'. Temporary one has been made, but be sure to add one after debugging.");
//        }

//        P_Interact.OnItemPlaceOrTake += AddItemCheck;
//    }

//    private void AddNewItemToCauldron(GameObject gameobject)
//    {
//        ItemObject itemObj = gameobject.GetComponent<ItemObject>();

//        if (itemObj == null)
//        {
//            return;
//        }

//        gameobject.transform.position = itemHolder.transform.position;
//        gameobject.transform.parent = itemHolder.transform;
//        gameobject.GetComponent<MeshRenderer>().enabled = false;

//        HeldItems.Append(gameobject);

//    }

//    public void AddItemCheck(GameObject playerHeldItem, GameObject playerItemHolder, GameObject CorrectInteractable)
//    {
//        if (CorrectInteractable != gameObject)
//        {
//            return;
//        }
//        AddNewItemToCauldron(playerHeldItem);
//    }

//    public void MixCauldron()
//    {
//        if (HeldItems.Length < 1) { return; }

//        // Add stuff here about recipes

//        DeleteItems();
//    }

//    private void DeleteItems()
//    {
//        HeldItems = new GameObject[0];

//        foreach (Transform child in itemHolder.transform)
//        {
//            Destroy(child.gameObject);
//        }
//    }
//}
