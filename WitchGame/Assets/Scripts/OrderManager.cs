using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    [SerializeField] private RecipeListSO _recipeListSO;
    private List<Order> _orderList;
    public IReadOnlyList<Order> OrderList => _orderList as IReadOnlyList<Order>;

    [SerializeField] private float _orderSpawnTimerDuration = 5f;
    private float _orderSpawnTimer = 0;
    private int _nextOrderId = 0;
    [SerializeField] private float _orderTimeLimit = 10f;
    [SerializeField] private int _orderMaxAmount = 5;

    public static event Action<Order> OnOrderSpawned;
    public static event Action<Order> OnOrderDelivered;
    public static event Action OnInvalidOrderDelivery;
    public static event Action<Order> OnOrderExpired;

    private void Awake()
    {
        instance = this;
        _orderList = new List<Order>();
    }

    private void Update()
    {
        // Handle Each Order Timer
        for (int i = _orderList.Count - 1; i >= 0; i--)
        {
            Order order = _orderList[i];
            order.TickTimer(Time.deltaTime);
            if (order.IsTimerStopped())
            {
                //Debug.Log(i + ". Order Expired: " + order.recipe.name + " (Timer: " + order.GetTimer() + ")"); 
                _orderList.RemoveAt(i);
                OnOrderExpired?.Invoke(order);
            }
            else
            {
                //Debug.Log(i + ". " + order.recipe.name + " (Timer: " + order.GetTimer() + ")");
            }
        }

        // Order Spawning
        _orderSpawnTimer -= Time.deltaTime;
        if (_orderSpawnTimer <= 0f)
        {
            _orderSpawnTimer = _orderSpawnTimerDuration;

            if (_orderList.Count < _orderMaxAmount)
            {
                CreateRandomOrder();
            }
        }
    }

    private void CreateRandomOrder()
    {
        int index = UnityEngine.Random.Range(0, _recipeListSO.recipeList.Count);
        RecipeSO randomRecipe = _recipeListSO.recipeList[index];
        Order newOrder = new Order(randomRecipe, _nextOrderId, _orderTimeLimit);
        _nextOrderId++;
        _orderList.Add(newOrder);
        OnOrderSpawned?.Invoke(newOrder);
        //foreach (KeyValuePair<Item, int> pair in randomRecipe.IngredientCounts)
        //{
        //    Debug.Log("Order Recipe Dictionary: Key = " + pair.Key + ", Value = " + pair.Value);
        //}
    }

    public bool deliverOrderRecipe(ItemObject deliveredItem)
    {
        string recipeName = deliveredItem.name;

        for (int i = _orderList.Count - 1; i >= 0; i--)
        {
            if (recipeName == _orderList[i].recipe.name)
            {
                OnOrderDelivered?.Invoke(_orderList[i]);
                Debug.Log("Order sent: " + _orderList[i].recipe.name);
                _orderList.RemoveAt(i);
                return true;
            }
        }

        OnInvalidOrderDelivery?.Invoke();
        return false;
    }

    public float GetTimeLimit()
    {
        return _orderTimeLimit;
    }
}
