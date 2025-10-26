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

        workshopTool.UseItem += SetNewTimer;
        workshopTool.TookItem.AddListener(ResetTimer);

        OnComplete += workshopTool.ConvertItem;

        P_Interact.OnInteract += OnPlayerInteractTool;
    }

    private void Update()
    {
        if (!requiresPlayerInput && !isDone && workshopTool.currentItemObjectInUse != null)
        {
            AutoTime();
        }
        if (slider != null)
        {
            slider.value = currentTime / ToolUseDurationInSeconds;
        }
        if (workshopTool.currentItemObjectInUse == null)
        {
            ResetTimer();
        }
    }

    public void SetNewTimer()
    {
        Debug.Log("Setting new timer...");
        currentTime = 0f;
        isDone = false;

        canvas.enabled = true;

        if (workshopTool.currentItemObjectInUse == null)
        {
            ResetTimer();
        }
    }

    private void AutoTime()
    {
        IncremenentTime();
    }

    public void OnPlayerInteractTool(GameObject CorrectInteractable)
    {
        if (!requiresPlayerInput || isDone) {  return; }
        if (workshopTool.currentItemObjectInUse == null) { return; }
        Debug.Log("Player interacting with object");
        if (CorrectInteractable != gameObject)
        {
            Debug.Log("CorrectInteractable does not match gameObject");
            return;
        }
        IncremenentTime(0.5f, false);
    }

    private void IncremenentTime(float increment=1f, bool useTime=true)
    {
        currentTime += increment * (useTime?Time.deltaTime:1f);
        Debug.Log("Current Time: " + currentTime);

        if (currentTime >= ToolUseDurationInSeconds)
        {
            TimerComplete();
        }
    }

    private void TimerComplete()
    {
        Debug.Log("Timer complete!");
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
