using UnityEngine;

public class P_Interact : MonoBehaviour
{
    public float playerAcitvateDistance;
    bool active = false;

    public GameObject P_ItemHolder;
    GameObject HeldItem;
    GameObject Currentinteractable;

    private void Start()
    {
        InputController.onPlayerInteract += Interact;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactable")
        {
            RaycastHit hit;
            active = Physics.Raycast(transform.position, transform.forward, out hit, playerAcitvateDistance);
            if (active)
            {
                Currentinteractable = other.gameObject;
            }
        }
    }

    void Interact()
    {
        if (!active) { return; }

        ItemHolder holder = Currentinteractable.GetComponent<ItemHolder>();
        ItemCrate crate = Currentinteractable.GetComponent<ItemCrate>();

        if (holder)
        {
            holder.TakeOrPlaceItemCheck(HeldItem);
        }

        if (crate)
        {
            crate.TakeItemCheck(HeldItem);
        }

        HeldItem.transform.parent = P_ItemHolder.transform;
        HeldItem.transform.position = P_ItemHolder.transform.position;
        
    }
}
