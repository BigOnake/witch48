using UnityEngine;

public class Order
{
    public RecipeSO recipe { get; private set; }
    public int id { get; private set; }
    private float _orderTimer;

    public Order(RecipeSO r, int i, float duration = 10f)
    {
        this.recipe = r;
        this.id = i;
        this._orderTimer = duration;
    }

    public void TickTimer(float deltaTime)
    {
        if (IsTimerStopped())
        {
            return;
        }

        _orderTimer -= deltaTime;
    }

    public bool IsTimerStopped()
    {
        return _orderTimer <= 0f;
    }

    public float GetTimer()
    {
        return _orderTimer;
    }
}
