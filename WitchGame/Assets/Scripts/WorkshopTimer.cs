// May only be placed where the WorkshopTool is.

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorkshopTimer : MonoBehaviour
{
    public bool requiresPlayerInput = false;

    public float ToolUseDurationInSeconds = 5f;

    float currentTime = 0f;

    [HideInInspector] public bool isDone = true;

    public event Action OnStart;
    public event Action OnComplete;

    WorkshopTool workshopTool;
    Canvas canvas;
    Slider slider;

    private void Start()
    {
        workshopTool = GetComponent<WorkshopTool>();

        if (workshopTool == null )
        {
            Debug.LogWarning("WorkshopTimer cannot find WorkshopTool. Are they inside the same gameObject?");
        }

        canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = false;
            slider = canvas.gameObject.GetComponentInChildren<Slider>();
        }

        workshopTool.UseItem += OnStart;
        workshopTool.TookItem.AddListener(ResetTimer);

        OnStart += SetNewTimer;
        OnComplete += workshopTool.ConvertItem;

        P_Interact.OnInteract += OnPlayerInteractTool;
    }

    private void Update()
    {
        Debug.Log("Requiers Player Input: " + requiresPlayerInput);
        Debug.Log("isDone: " + isDone);
        if (!requiresPlayerInput && !isDone)
        {
            AutoTime();
        }
        if (slider != null)
        {
            slider.value = currentTime / ToolUseDurationInSeconds;
        }
    }

    public void SetNewTimer()
    {
        currentTime = 0f;
        isDone = false;

        canvas.enabled = true;
    }

    private void AutoTime()
    {
        IncremenentTime();
    }

    public void OnPlayerInteractTool(GameObject CorrectInteractable)
    {
        if (CorrectInteractable != gameObject)
        {
            Debug.Log("CorrectInteractable does not match gameObject");
            return;
        }
        IncremenentTime();
    }

    private void IncremenentTime()
    {
        currentTime += 0.1f * Time.deltaTime;
        Debug.Log("Current Time: " + currentTime);

        if (currentTime >= ToolUseDurationInSeconds)
        {
            TimerComplete();
        }
    }

    private void TimerComplete()
    {
        isDone = true;
        canvas.enabled = false;
        OnComplete.Invoke();
    }

    void ResetTimer()
    {
        currentTime = 0f;
        canvas.enabled = false;
        isDone = true;
    }
}
