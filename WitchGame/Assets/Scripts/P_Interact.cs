using System;
using UnityEngine;

public class P_Interact : MonoBehaviour
{
    public float playerAcitvateDistance;
    bool active = false;

    public GameObject P_ItemHolder;
    GameObject HeldItem;
    public static GameObject Currentinteractable;

    public static event Action<GameObject, GameObject, GameObject> OnItemPlaceOrTake;
    public static event Action<GameObject> OnInteract;

    private void Start()
    {
        InputController.onPlayerInteract += Interact;
        InputController.onPlayerGetItem += PlaceOrTake;
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

        OnInteract.Invoke(Currentinteractable);
    }

    void PlaceOrTake()
    {
        if (!active) { return; }

        OnItemPlaceOrTake.Invoke(HeldItem, P_ItemHolder, Currentinteractable);

        if (P_ItemHolder.transform.childCount < 1)
        {
            HeldItem = null;
            return;
        }

        HeldItem.transform.position = P_ItemHolder.transform.position;
    }
}
