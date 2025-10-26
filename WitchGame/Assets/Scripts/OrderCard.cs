using UnityEngine;
using UnityEngine.UI;

public class OrderCard : MonoBehaviour
{
    public Order order;

    Slider orderTimer;
    Image orderIcon;

    float TimeLimit;

    float id;

    private void Start()
    {
        TimeLimit = OrderManager.instance.GetTimeLimit();
        orderTimer = GetComponentInChildren<Slider>();
        orderIcon = GetComponentInChildren<Image>();

        id = order.id;

        if (order.recipe.recipeIcon != null )
        {
            orderIcon.sprite = order.recipe.recipeIcon;
        }

        OrderManager.OnOrderExpired += DestroyCard;
        OrderManager.OnOrderDelivered += DestroyCard;
    }

    private void Update()
    {
        // If it's null, then the order must have been destroyed. Thus, the card should also be destroyed.
        if (order != null)
        {
            orderTimer.value = order.GetTimer() / TimeLimit;
        } else
        {
            Destroy(gameObject);
        }
    }

    void DestroyCard(Order D_order)
    {
        if (id == D_order.id)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OrderManager.OnOrderExpired -= DestroyCard;
        OrderManager.OnOrderDelivered -= DestroyCard;
    }

}
