using UnityEngine;
using System;
using System.Collections;

public class CharacterCustomisationEventHub : MonoBehaviour
{
    public const string randomiseEventName = "randomisecharacter";
    public const string resetEventName = "resetcharacter";
    public const string saveEventName = "savecharacter";
    public const string loadEventName = "loadcharacter";

    [SerializeField] private GameObject characterBuilderPrefab;
    private CharacterBuilder characterBuilder;

    private void Start()
    {
        EventManager.Instance.AddEvent(randomiseEventName, new CustomEvent());
        EventManager.Instance.AddEvent(resetEventName, new CustomEvent());
        EventManager.Instance.AddEvent(saveEventName, new CustomEvent());
        EventManager.Instance.AddEvent(loadEventName, new CustomEvent());
    }

    public void RandomiseCharacter()
    {
        EventManager.Instance.InvokeEvent(randomiseEventName);
    }

    public void ResetCharacter()
    {
        EventManager.Instance.InvokeEvent(resetEventName);
    }

    public void SaveCharacter()
    {
        // Have to instantiate at runtime as otherwise we get order of operation errors in Start
        if (characterBuilder == null)
        {
            characterBuilder = Instantiate(characterBuilderPrefab).GetComponent<CharacterBuilder>();
            Debug.Log($"CharacterBuilder prefab instantiated.");
        }
        EventManager.Instance.InvokeEvent(saveEventName);
    }

    public void LoadCharacter()
    {
        EventManager.Instance.InvokeEvent(loadEventName);
    }
}
