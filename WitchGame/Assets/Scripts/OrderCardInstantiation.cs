//To be attached to Card Canvas
using UnityEngine;
using UnityEngine.UI;

public class OrderCardInstantiation : MonoBehaviour
{
    [SerializeField] private GameObject OrderCardPrefab;
    [SerializeField] private GameObject IngredientIconPrefab;

    private GameObject orderList;

    private void Start()
    {
        CreateOrderList();
        OrderManager.OnOrderSpawned += CreateOrderCard;
    }
    public void CreateOrderCard(Order order)
    {
        GameObject orderCard = Instantiate(OrderCardPrefab);

        orderCard.transform.parent = orderList.transform;

        orderCard.GetComponent<OrderCard>().order = order;

        var ingredientPanel = orderCard.transform.Find("Ingredients Panel").gameObject;

        foreach (var ingredient in order.recipe.ingredientSOList)
        {
            var ingIcon = Instantiate(IngredientIconPrefab);
            ingIcon.transform.parent = ingredientPanel.transform;
            ingIcon.GetComponent<Image>().sprite = ingredient.ItemIcon;
        }
    }

    public void CreateOrderList()
    {
        orderList = gameObject.transform.GetChild(0).gameObject;
    }
}
