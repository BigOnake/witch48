using UnityEngine;

public interface IInteractable
{
    void Interact(P_Interact player);
    void PlaceOrTake(P_Interact player);
}
