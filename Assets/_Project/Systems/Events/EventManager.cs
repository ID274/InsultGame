using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // This event manager class is a singleton in charge of managing all the events in the game. It uses the CustomEvent
    // class for event handling. This class has no dependencies, so lower level modules can ask for an event to be added,
    // without it caring about the details of how the event is implemented.

    public static EventManager Instance { get; private set; }

    private Dictionary<string, CustomEvent> events = new Dictionary<string, CustomEvent>();

    [SerializeField] private bool createEventIfMissing = true;
    [SerializeField] private bool dontDestroyOnLoad = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void InvokeEvent(string eventName)
    {
        eventName = eventName.ToLower();
        if (events.TryGetValue(eventName, out CustomEvent baseEvent))
        {
            baseEvent.Invoke();
        }
        else
        {
            Debug.LogWarning("Event with name " + eventName + " was not found.");
        }
    }

    public void SubscribeToEvent(string eventName, Action action)
    {
        eventName = eventName.ToLower();
        if (events.TryGetValue(eventName, out CustomEvent customEvent))
        {
            customEvent.OnEventTriggered += action;
        }
        else
        {
            Debug.LogWarning("Event with name " + eventName + " was not found.");
            if (createEventIfMissing)
            {
                Debug.Log("Creating a new one to allow subscription.");
                CustomEvent newEvent = new CustomEvent();
                events.Add(eventName, newEvent);
                newEvent.OnEventTriggered += action;
            }
        }
    }

    public void UnsubscribeFromEvent(string eventName, Action action)
    {
        eventName = eventName.ToLower();
        if (events.TryGetValue(eventName, out CustomEvent customEvent))
        {
            customEvent.OnEventTriggered -= action;
        }
        else
        {
            Debug.LogWarning("Event with name " + eventName + " was not found.");
        }
    }

    public void AddEvent(string eventName, CustomEvent baseEvent)
    {
        eventName = eventName.ToLower();
        if (!events.ContainsKey(eventName))
        {
            events.Add(eventName, baseEvent);
            Debug.Log($"Event with name {eventName} added.");
        }
        else
        {
            Debug.LogWarning("Event with name " + eventName + " already exists.");
        }
    }

    public void RemoveEvent(string eventName)
    {
        eventName = eventName.ToLower();
        if (events.ContainsKey(eventName))
        {
            events.Remove(eventName);
        }
        else
        {
            Debug.LogWarning("Event with name " + eventName + " was not found.");
        }
    }
}
